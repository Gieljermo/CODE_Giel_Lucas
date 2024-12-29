using Controlllers;
using Domain;
using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Decorators;
using TempleOfDoom.view;

namespace TempleOfDoom.controller
{
    public class BoardController
    {
        private Board board { get; set; }
        public Player _player { get; set; }
        public Room _gameRoom { get; set; }

        public Room[] rooms = new Room[10];

        private FieldController fieldController;

        private GameController _gameController;


        public BoardController(GameController controller, FieldController fieldController)
        {
            _gameController = controller;
            this.fieldController = fieldController;
        }

        public void DrawRoom()
        {
            Field currentPlayerField = _gameRoom.Fields.Where(f => f.Y == _player.YPositon).FirstOrDefault(f => f.X == _player.XPositon);
            if (currentPlayerField != null && currentPlayerField.IsConnection != 0)
            {
                Field fieldToMoveTo = _gameRoom.Fields.Where(f => f.Y == _player.YPositon).FirstOrDefault(f => f.X == _player.XPositon);
                if (fieldToMoveTo != null && fieldToMoveTo.Connection != null && fieldToMoveTo.Connection.Portals != null)
                {
                    Portal newPortal = fieldToMoveTo.Connection.Portals.FirstOrDefault(f => f.RoomId != _gameRoom.Id);
                    if (newPortal != null)
                    {
                        _player.XPositon = newPortal.X;
                        _player.YPositon = newPortal.Y;
                    }
                }
                _gameRoom = rooms[currentPlayerField.IsConnection];
                if (_gameRoom == null)
                {
                    _gameRoom = _gameController.TempleOfDoomGame.Rooms.Where(r => r.Id == currentPlayerField.IsConnection).FirstOrDefault();
                    CreateRoom(_gameRoom);
                }


                Field newRoomDoor = _gameRoom.Fields.Where(f => f.IsConnection == currentPlayerField.Room).FirstOrDefault();
                if (newRoomDoor != null)
                {
                    _player.XPositon = newRoomDoor.X;
                    _player.YPositon = newRoomDoor.Y;
                }
            }

            board.DrawRoom(_gameRoom, _player);
            _gameController.CheckGameStatus(_player);
        }

        // CreateStartingRoom now receives starting room data as parameters instead of relying on JSON
        public void CreateStartingRoom(Player player, Room startingRoom)
        {
            _player = player;
            _gameRoom = CreateRoom(startingRoom);

            board = new Board();
        }

        // This version of CreateRoom takes a Room directly and initializes it
        public Room CreateRoom(Room room)
        {
            Room gameRoom = room;
            List<Connection> connections = _gameController.TempleOfDoomGame.Connections.ToList();
            gameRoom.Fields = fieldController.CreateFields(room, _player, connections);

            foreach (var item in _gameController.TempleOfDoomGame.Connections)
            {
                if (item.North == gameRoom.Id || item.South == gameRoom.Id || item.West == gameRoom.Id || item.East == gameRoom.Id)
                {
                    gameRoom.Connections.Add(item);
                    gameRoom.Fields = AddDoor(item, gameRoom);
                }
            }

            rooms[gameRoom.Id] = gameRoom;

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

                // Determine the position of the door based on room and connection layout
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

                // If no portal was found, follow the regular connection logic
                if (item.X == width && item.Y == height)
                {
                    item.Door = connection.Door;
                    item.IsWall = false;
                    item.IsConnection = nextRoomId;  // Set the next room ID based on the connection direction
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

        public bool CanMoveTo(int x, int y)
        {
            Field fieldToMoveTo = _gameRoom.Fields.Where(f => f.Y == y).FirstOrDefault(f => f.X == x);
            if (fieldToMoveTo == null || fieldToMoveTo.IsWall)
            {
                return false;
            }

            if (fieldToMoveTo.Connection != null && fieldToMoveTo.Connection.Portals != null)
            {
                Portal availablePortal = fieldToMoveTo.Connection.Portals.FirstOrDefault(f => f.RoomId != _gameRoom.Id);
                if (availablePortal != null)
                {
                    fieldToMoveTo.IsConnection = availablePortal.RoomId;
                    //fieldToMoveTo.Room = availablePortal.RoomId;
                    //fieldToMoveTo.X = availablePortal.X;
                    //fieldToMoveTo.Y = availablePortal.Y;


                    return true;
                }
            }


            IDoor door = GetDoor(x, y);
            if (door != null)
            {
                door.OpenDoor(_player, _gameRoom);
                return door.IsOpen;

            }


            return true;
        }

        public IItem GetItemAtPosition(int x, int y)
        {
            Field fieldToCheck = _gameRoom.Fields.Where(f => f.X == x && f.Y == y).FirstOrDefault();
            if(fieldToCheck == null)
            {
                return null;
            }

            return fieldToCheck.Item;
        }

        public IDoor GetDoor(int x, int y)
        {
            Field fieldToCheck = _gameRoom.Fields.Where(f => f.X == x && f.Y == y).FirstOrDefault();
            if (fieldToCheck == null)
            {
                return null;
            }

            return fieldToCheck.Door;
        }

    }
}
