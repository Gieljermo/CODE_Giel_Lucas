using TempleOfDoom.model;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using TempleOfDoom.model.Factory;

namespace TempleOfDoom.controller
{
    public class GameController
    {
        public TempleOfDoomGame TempleOfDoomGame { get; set; }
        private bool gameRunning = true;
        private BoardController boardController;
        private PlayerController playerController;

        public GameController(TempleOfDoomGameJson data)
        {
            this.TempleOfDoomGame = GenerateGameClasses(data);
            this.boardController = new BoardController(this);
            this.playerController = new PlayerController(TempleOfDoomGame.Player);


            var startingRoom = TempleOfDoomGame.Rooms.FirstOrDefault(r => r.Id == TempleOfDoomGame.Player.StartRoomId);
            if (startingRoom != null)
            {
                boardController.CreateStartingRoom(TempleOfDoomGame.Player, startingRoom);
            }

            Loop();
        }

        public void Loop()
        {
            while (gameRunning)
            {
                boardController.DrawRoom();
                ConsoleKey key = Console.ReadKey(true).Key;
                playerController.Move(key, boardController._gameRoom);
            }
        }


        public TempleOfDoomGame GenerateGameClasses(TempleOfDoomGameJson data)
        {
            List<Room> rooms = data.rooms.Select(roomJson =>
            {
                // Initialize room
                var room = new Room(
                    roomJson.id,
                    roomJson.type,
                    roomJson.width,
                    roomJson.height
                );

                // Populate the room's items
                if (roomJson.items != null)
                {
                    room.Items = roomJson.items.Select(itemJson => new Item(
                        itemJson.type,
                        itemJson.damage,
                        itemJson.x,
                        itemJson.y,
                        itemJson.color
                    )).ToList();
                }

                return room;
            }).ToList();




            // Convert connections
            List<Connection> connections = data.connections.Select(connectionJson =>
            {
                var connection = new Connection(
                    connectionJson.NORTH,
                    connectionJson.WEST,
                    connectionJson.SOUTH,
                    connectionJson.EAST
                );

                DoorFactory doorFactory = new DoorFactory();

                // If doors are defined, create the decorated door
                if (connectionJson.doors != null && connectionJson.doors.Any())
                {
                    connection.Door = doorFactory.CreateDecoratedDoor(connectionJson.doors);
                }

                return connection;
            }).ToList();

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
