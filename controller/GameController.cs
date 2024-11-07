using TempleOfDoom.model;
using System.Collections.Generic;
using System.Linq;

namespace TempleOfDoom.controller
{
    public class GameController
    {
        public TempleOfDoomGame TempleOfDoomGame { get; set; }
        private bool gameRunning = true;
        private BoardController boardController;

        public GameController(TempleOfDoomGameJson data)
        {
            boardController = new BoardController(this);
            this.TempleOfDoomGame = GenerateGameClasses(data);

            var startingRoom = TempleOfDoomGame.Rooms.FirstOrDefault(r => r.Id == TempleOfDoomGame.Player.StartRoomId);
            if (startingRoom != null)
            {
                boardController.CreateStartingRoom(TempleOfDoomGame.Player, startingRoom);
            }

            // Game loop
            while (gameRunning)
            {
                boardController.DrawRoom();
            }
        }


        public TempleOfDoomGame GenerateGameClasses(TempleOfDoomGameJson data)
        {
            // Convert rooms
            List<Room> rooms = data.rooms.Select(roomJson => new Room(
                roomJson.id,
                roomJson.type,
                roomJson.width,
                roomJson.height
            )).ToList();

            // Convert connections
            List<Connection> connections = data.connections.Select(connectionJson => new Connection(
                connectionJson.NORTH,
                connectionJson.WEST,
                connectionJson.SOUTH,
                connectionJson.EAST
            )).ToList();


            // Convert player
            Player player = new Player(
                data.player.startRoomId,
                data.player.startX,
                data.player.startY,
                data.player.lives
            );

            // Create and return the TempleOfDoomGame domain object
            return new TempleOfDoomGame(rooms, connections, player);
        }

        public void CheckGameStatus(Player player)
        {
            if (player.AmountOfLives <= 0)
            {
                gameRunning = false;
                boardController.DrawLosingScreen();
                return;
            }

            if (player.AmountOfStones >= 5)
            {
                gameRunning = false;
                boardController.DrawWinScreen();
                return;
            }
        }
    }
}
