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

        public List<Room> rooms = new List<Room>();

        private FieldController fieldController;

        private GameController _gameController;


        public BoardController(GameController controller, FieldController fieldController)
        {
            _gameController = controller;
            this.fieldController = fieldController;
        }

        public void DrawRoom()
        {
            Field currentPlayerField = _gameRoom.Fields.Where(f => f.Y == _player.Y).FirstOrDefault(f => f.X == _player.X);
            if (currentPlayerField != null && currentPlayerField.IsConnection != 0)
            {
                _gameRoom = rooms.Where(r => r.Id == currentPlayerField.IsConnection).FirstOrDefault();
                if (_gameRoom == null)
                {
                    _gameRoom = _gameController.TempleOfDoomGame.Rooms.Where(r => r.Id == currentPlayerField.IsConnection).FirstOrDefault();
                    CreateRoom(_gameRoom);
                }
               
                Field newRoomDoor = _gameRoom.Fields.Where(f => f.IsConnection == currentPlayerField.Room.Id).FirstOrDefault();
                if (newRoomDoor != null)
                {
                    _player.X = newRoomDoor.X;
                    _player.Y = newRoomDoor.Y;
                }
            }

            board.DrawRoom(_gameRoom, _player);
            _gameController.CheckGameStatus(_player);
        }

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
            gameRoom.Fields = fieldController.CreateFields(room, _player);

            foreach (var item in _gameController.TempleOfDoomGame.Connections)
            {
                if (item.North == gameRoom.Id || item.South == gameRoom.Id || item.West == gameRoom.Id || item.East == gameRoom.Id)
                {
                    gameRoom.Connections.Add(item);
                    gameRoom.Fields = AddDoor(item, gameRoom);
                }

                if(item.Upper == gameRoom.Id || item.Lower == gameRoom.Id)
                {
                    gameRoom.Connections.Add(item);
                    gameRoom.Fields = AddLadder(item, gameRoom);
                }
            }

            rooms.Add(gameRoom);

            return gameRoom;
        }

        private List<Field> AddLadder(Connection connection, Room gameRoom)
        {
            foreach (var item in gameRoom.Fields)
            {
                int width = 0;
                int height = 0;
                int nextRoomId = 0;

                if (connection.Upper == gameRoom.Id)
                {
                    width = connection.ladder.UpperX;
                    height = connection.ladder.UpperY;
                    nextRoomId = connection.Lower;
                }
                else if (connection.Lower == gameRoom.Id)
                {
                    width = connection.ladder.LowerX;
                    height = connection.ladder.LowerY;
                    nextRoomId = connection.Upper;
                }

                if (item.X == width && item.Y == height)
                {
                    item.isLadder = true;
                    item.IsWall = false;
                    item.IsConnection = nextRoomId;
                }
            }

            return gameRoom.Fields;
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


                if (item.X == width && item.Y == height)
                {
                    item.Door = connection.Door;
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

        public bool CanMoveTo(int x, int y)
        {
            Field fieldToMoveTo = _gameRoom.Fields.Where(f => f.Y == y).FirstOrDefault(f => f.X == x);
            if (fieldToMoveTo == null || fieldToMoveTo.IsWall)
            {
                return false;
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
