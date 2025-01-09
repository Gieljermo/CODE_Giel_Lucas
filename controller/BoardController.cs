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
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Decorators;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.view;

namespace TempleOfDoom.controller
{
    public class BoardController
    {
        public const int MAX_ROOMS = 10;
        private const int HALF_ROOM_WIDTH = 2;
        private const int HALF_ROOM_HEIGHT = 2;
        private const int ROOM_DIMENSION_OFFSET = 1;

        private BoardView board { get; set; }
        public Player Player { get; set; }
        public Room CurrentRoom { get; set; }

        public Room[] rooms = new Room[MAX_ROOMS];

        private FieldController fieldController;

        private GameController _gameController;


        public BoardController(GameController controller, FieldController fieldController)
        {
            _gameController = controller;
            this.fieldController = fieldController;
        }

        public void DrawRoom()
        {
            Field currentField = GetCurrentField();

            if (currentField == null || currentField.IsConnection == 0)
            {
                RenderRoom();
                return;
            }

            ProcessRoomChange(currentField);
            RenderRoom();
        }

        private Field GetCurrentField()
        {
            var field = CurrentRoom.Fields.FirstOrDefault(f =>
                f.Position.X == Player.Position.X &&
                f.Position.Y == Player.Position.Y);

            return field;
        }

        private void ProcessRoomChange(Field currentField)
        {
            Field portalField = GetPortalField(currentField);
            if (portalField != null)
            {
                MovePlayerToPortal(portalField);
            }

            UpdateCurrentRoom(currentField.IsConnection);

            Field newRoomDoor = GetDoorToPreviousRoom(currentField.Room.Id);
            if (newRoomDoor != null)
            {
                Player.Position = newRoomDoor.Position;
            }
        }

        private Field GetPortalField(Field currentField)
        {
            var field = CurrentRoom.Fields.FirstOrDefault(f =>
                f.Position.X == Player.Position.X &&
                f.Position.Y == Player.Position.Y &&
                f.Connection?.Portals != null);

            return field;
        }

        private void MovePlayerToPortal(Field portalField)
        {
            var newPortal = portalField.Connection.Portals
                .FirstOrDefault(p => p.RoomId != CurrentRoom.Id);

            if (newPortal != null)
            {
                Player.Position = newPortal.Position;
            }
        }

        private void UpdateCurrentRoom(int newRoomId)
        {
            if (rooms[newRoomId] != null)
            {
                CurrentRoom = rooms[newRoomId];
            }
            else
            {
                CurrentRoom = LoadRoomFromGame(newRoomId);
            }
        }

        private Room LoadRoomFromGame(int roomId)
        {
            var room = _gameController.TempleOfDoomGame.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                return CreateRoom(room);
            }

            return null;
        }


        private Field GetDoorToPreviousRoom(int previousRoomId)
        {
            var field = CurrentRoom.Fields.FirstOrDefault(f => f.IsConnection == previousRoomId);
            return field;
        }

        private void RenderRoom()
        {
            board.DrawRoom(CurrentRoom, Player);
            _gameController.CheckGameStatus(Player);
        }

        public void CreateStartingRoom(Player player, Room startingRoom)
        {
            Player = player;
            CurrentRoom = CreateRoom(startingRoom);

            board = new BoardView();
        }

        public Room CreateRoom(Room room)
        {
            Room gameRoom = room;
            List<Connection> connections = _gameController.TempleOfDoomGame.Connections.ToList();
            gameRoom.Fields = fieldController.CreateFields(room, Player, connections);

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

        //Adds a door to a field
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
                    width = (room.Width - ROOM_DIMENSION_OFFSET) / HALF_ROOM_WIDTH;
                    height = room.Height - ROOM_DIMENSION_OFFSET;
                    nextRoomId = connection.South;
                }
                else if (connection.East == room.Id)
                {
                    width = 0;
                    height = (room.Height - ROOM_DIMENSION_OFFSET) / HALF_ROOM_HEIGHT;
                    nextRoomId = connection.West;
                }
                else if (connection.South == room.Id)
                {
                    width = (room.Width - ROOM_DIMENSION_OFFSET) / HALF_ROOM_WIDTH;
                    height = 0;
                    nextRoomId = connection.North;
                }
                else if (connection.West == room.Id)
                {
                    width = room.Width - ROOM_DIMENSION_OFFSET;
                    height = (room.Height - ROOM_DIMENSION_OFFSET) / HALF_ROOM_HEIGHT;
                    nextRoomId = connection.East;
                }

                if (item.Position.X == width && item.Position.Y == height)
                {
                    item.Doors = connection.Doors;
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

        public bool CanMoveTo(IPosition position)
        {
            var fieldToMoveTo = GetFieldAtPosition(position);

            if (fieldToMoveTo == null || fieldToMoveTo.IsWall)
            {
                return false;
            }

            if (CanTeleport(fieldToMoveTo))
            {
                return true;
            }

            if (CanMoveThroughDoors(position))
            {
                return true;
            }

            return false;
        }

        private Field GetFieldAtPosition(IPosition position)
        {
            var field = CurrentRoom.Fields.FirstOrDefault(f => f.Position.Equals(position));

            return field;
        }

        private bool CanTeleport(Field fieldToMoveTo)
        {
            if (fieldToMoveTo.Connection?.Portals != null)
            {
                var availablePortal = fieldToMoveTo.Connection.Portals.FirstOrDefault(f => f.RoomId != CurrentRoom.Id);
                if (availablePortal != null)
                {
                    fieldToMoveTo.IsConnection = availablePortal.RoomId;
                    return true;
                }
            }

            return false;
        }

        private bool CanMoveThroughDoors(IPosition position)
        {
            var doors = GetDoors(position);

            if (doors == null)
            {
                return true;
            }

            foreach (var door in doors)
            {
                door.ChangeDoorStatus(Player, CurrentRoom);

                if (!door.IsOpen)
                {
                    return false;
                }
            }

            return true;
        }


        public IInteractiveFieldElement GetItemAtPosition(IPosition position)
        {
            var fieldToCheck = CurrentRoom.Fields.Where(f => f.Position.Equals(position)).FirstOrDefault();
            if (fieldToCheck == null)
            {
                return null;
            }

            return fieldToCheck.InteractiveFieldElement;
        }

        public List<IDoor> GetDoors(IPosition position)
        {
            var fieldToCheck = CurrentRoom.Fields.Where(f => f.Position.Equals(position)).FirstOrDefault();
            if (fieldToCheck == null)
            {
                return null;
            }

            return fieldToCheck.Doors;
        }

    }
}
