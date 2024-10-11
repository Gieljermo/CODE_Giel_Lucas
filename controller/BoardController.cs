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
        public TempleOfDoomGameJson _json { get; set; }

        public Player _player { get; set; }
        public Room _gameRoom { get; set; }
        public Room[] rooms = new Room[10];

        private FieldController fieldController = new FieldController();

        private GameController _gameController;

        public BoardController(GameController controller, TempleOfDoomGameJson json)
        {
            _json = json;
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

        public void CreateStartingRoom(PlayerJson player)
        {
            _player = new Player ( player.startRoomId, player.startX, player.startY, 3 );
            _gameRoom = CreateRoom(_json.rooms.Where(r => r.id == _json.player.startRoomId).FirstOrDefault());
            rooms[_json.player.startRoomId] = _gameRoom;
            fieldController.AddPlayer(_gameRoom, _player);

            board = new Board();
        }

        // Creates a gameroom from a jsonroom and connections

        public Room CreateRoom(RoomJson room)
        {
            Room gameRoom = new Room(room.id, room.type, room.width, room.height);
            gameRoom.Fields =  fieldController.CreateFields(room, _player);

            foreach (var item in _json.connections)
            {

                if (item.NORTH == gameRoom.Id || item.SOUTH == gameRoom.Id || item.WEST == gameRoom.Id || item.EAST == gameRoom.Id)
                {
                    var connection = new Connection(item.EAST, item.NORTH, item.SOUTH, item.WEST);

                    gameRoom.Connections.Add(connection);

                    gameRoom.Fields = AddDoor(item, gameRoom);

                }

            }
            return gameRoom;
        }

        /// <summary>
        /// This method add a door to the room. 
        /// As a result of, one piece of the wall will remove
        /// The door will display in the center of the wall always, where the connection is.
        /// </summary>
        public List<Field> AddDoor(ConnectionJson connection, Room room)
        {
            foreach (var item in room.Fields)
            {
                var width = 0;
                var height = 0;
                var nextroomid = 0;
                if (connection.NORTH == room.Id)
                {
                    width = (room.Width - 1) / 2;
                    height = room.Height - 1;
                    nextroomid = connection.SOUTH;
                }
                if (connection.EAST == room.Id)
                {
                    width = 0;
                    height = (room.Height - 1) / 2;
                    nextroomid = connection.WEST;
                }
                if (connection.SOUTH == room.Id)
                {
                    width = (room.Width - 1) / 2;
                    height = 0;
                    nextroomid = connection.NORTH;
                }
                if (connection.WEST == room.Id)
                {
                    width = room.Width - 1;
                    height = (room.Height - 1) / 2;
                    nextroomid = connection.EAST;
                }
                //Search the center of the wall
                if (item.X == width && item.Y == height)
                {
                    item.IsWall = false;
                    item.IsConnection = nextroomid;
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
