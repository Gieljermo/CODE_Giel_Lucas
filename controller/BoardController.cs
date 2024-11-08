using Controlllers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.view;

namespace TempleOfDoom.controller
{
    public class BoardController
    {
        private Board board { get; set; }
        public Player _player { get; set; }
        public Room _gameRoom { get; set; }
        public Room[] rooms = new Room[10];

        private FieldController fieldController = new FieldController();

        private GameController _gameController;


        public BoardController(GameController controller)
        {
            _gameController = controller;
        }

        public void DrawRoom()
        {
            board.DrawRoom(_gameRoom, _player);

            var ch = board.checkForInput(_gameRoom);

            switch (ch)
            {
                case ConsoleKey.UpArrow:
                    Console.WriteLine("test");
                    break;
            }

            _gameController.CheckGameStatus(_player);
        }

        // CreateStartingRoom now receives starting room data as parameters instead of relying on JSON
        public void CreateStartingRoom(Player player, Room startingRoom)
        {
            _player = player;
            _gameRoom = CreateRoom(startingRoom);
            rooms[startingRoom.Id] = _gameRoom;
            fieldController.AddPlayer(_gameRoom, _player);

            board = new Board();
        }

        // This version of CreateRoom takes a Room directly and initializes it
        public Room CreateRoom(Room room)
        {
            Room gameRoom = room;
            gameRoom.Fields = fieldController.CreateFields(room, _player);

            foreach (var item in _gameController.TempleOfDoomGame.Connections)
            {
                if (item.North == gameRoom.Id || item.South == gameRoom.Id || item.West == gameRoom.Id || item.East == gameRoom.Id)
                {
                    gameRoom.Connections.Add(item);
                    gameRoom.Fields = AddDoor(item, gameRoom);
                }
            }

            return gameRoom;
        }


        /// <summary>
        /// Adds a door to the room by removing a part of the wall at the center of the wall where the connection is.
        /// </summary>
        public List<Field> AddDoor(Connection connection, Room room)
        {
            foreach (var item in room.Fields)
            {
                int width = 0;
                int height = 0;
                int nextRoomId = 0;

                if (connection.North == room.Id)
                {
                    width = (room.Width - 1) / 2;
                    height = room.Height - 1;
                    nextRoomId = connection.South;
                }
                else if (connection.East == room.Id)
                {
                    width = 0;
                    height = (room.Height - 1) / 2;
                    nextRoomId = connection.West;
                }
                else if (connection.South == room.Id)
                {
                    width = (room.Width - 1) / 2;
                    height = 0;
                    nextRoomId = connection.North;
                }
                else if (connection.West == room.Id)
                {
                    width = room.Width - 1;
                    height = (room.Height - 1) / 2;
                    nextRoomId = connection.East;
                }

                // Update the field to create a door
                if (item.X == width && item.Y == height)
                {
                    item.IsWall = false;
                    item.IsConnection = nextRoomId;
                }
            }

            return room.Fields;
        }

        public void DrawLosingScreen()
        {
            board.DrawLosingScreen();
        }

        public void DrawWinScreen()
        {
            board.DrawWinScreen();
        }
    }
}
