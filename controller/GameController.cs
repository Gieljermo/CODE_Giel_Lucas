using TempleOfDoom.model;

namespace TempleOfDoom.controller
{
    public class GameController
    {
        public TempleOfDoomGame TempleOfDoomGame {get; set;}
        private bool gameRunning = true;
        private BoardController boardController;

        public GameController(TempleOfDoomGameJson data)
        {
            boardController = new BoardController(this, data);
            this.TempleOfDoomGame = GenerateGameClasses(data);
            boardController.CreateStartingRoom(data.player);

            while (gameRunning)
            {
                boardController.DrawRoom();
            }
        }

        public TempleOfDoomGame GenerateGameClasses(TempleOfDoomGameJson data)
        {
            List<Room> rooms = data.rooms.Select(roomJson => new Room(
                   roomJson.id,
                   roomJson.type,
                   roomJson.width,
                   roomJson.height
               )).ToList();

            // Convert connections
            List<Connection> connections = data.connections.Select(connectionJson => new Connection(
                connectionJson.NORTH,
                connectionJson.SOUTH,
                connectionJson.WEST,
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
