using TempleOfDoom.model;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Controlllers;
using TempleOfDoom.model.Factory;
using TempleOfDoom.model.Interfaces;
using TempleOfDoom.model.Observers;
using Domain.Decorators;
using Domain.Factory;
using Domain;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.DTO;

namespace TempleOfDoom.controller
{
    public class GameController
    {
        public TempleOfDoomGame TempleOfDoomGame { get; set; }
        private bool gameRunning = true;
        private BoardController boardController;
        private PlayerController playerController;
        private OpponentController opponentController;
        private FieldController fieldController;
        private readonly TempleOfDoomGameJson _gameData;

        public GameController(IFileReader fileReader, string fileName)
        {
            this._gameData = fileReader.readFile(fileName);
            this.TempleOfDoomGame = GenerateGameClasses(_gameData);
            this.fieldController = new FieldController();
            this.boardController = new BoardController(this, this.fieldController);
            this.playerController = new PlayerController(TempleOfDoomGame.Player, boardController);
            this.opponentController = new OpponentController(boardController);

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
                playerController.CheckDamage(boardController._gameRoom);
                opponentController.Move(key, boardController._gameRoom);
                playerController.CheckDamage(boardController._gameRoom);
                opponentController.CheckDamage(key, boardController._gameRoom, TempleOfDoomGame.Player);



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

                FieldElementFactory itemFactory = new FieldElementFactory();

                // Populate the room's items
                if (roomJson.items != null)
                {
                    room.Items = roomJson.items.Select<ItemJson, Item>(itemJson =>
                    {
                        IPosition position = new Position(itemJson.x, itemJson.y);
                        return (Item)itemFactory.CreateItem(itemJson.type, position, itemJson.damage, itemJson.color);

                    }).ToList();
                }

                // Populate the room's items
                if (roomJson.specialFloorTiles != null)
                {
                    room.SpecialFloorTiles = roomJson.specialFloorTiles.Select(floorJson =>
                    {
                        // Probeer de string om te zetten naar de enum
                        if (Enum.TryParse<Direction>(floorJson.direction, true, out var direction))
                        {
                            IPosition position = new Position(floorJson.x, floorJson.y);

                            return new SpecialFloorTile(
                                floorJson.type,
                                position,
                                direction
                            );
                        }
                        else
                        {
                            throw new ArgumentException($"Ongeldige richting: {floorJson.direction}");
                        }
                    }).ToList();
                }

                // Populate the room's items
                if (roomJson.enemies != null)
                {
                    room.Opponents = roomJson.enemies.Select(opponentJson =>
                    {
                        // Maak een Position object en geef dit door aan de Portal constructor
                        IPosition portalPosition = new Position(opponentJson.x, opponentJson.y);
                        return new Opponent(
                        opponentJson.type,
                        portalPosition,
                        opponentJson.minX,
                        opponentJson.maxX,
                        opponentJson.minY,
                        opponentJson.maxY

                        );
                    }).ToList();
                }

                return room;
            }).ToList();

            Player player = new Player(
                data.player.startRoomId,
                data.player.startX,
                data.player.startY,
                data.player.lives
            );


            // Convert connections
            List<Connection> connections = data.connections.Select(connectionJson =>
            {
                var connection = new Connection(
                    connectionJson.NORTH,
                    connectionJson.WEST,
                    connectionJson.SOUTH,
                    connectionJson.EAST
                );

                // Populate the room's items
                if (connectionJson.portal != null)
                {
                    connection.Portals = connectionJson.portal.Select(portalJson =>
                    {
                        IPosition portalPosition = new Position(portalJson.x, portalJson.y);
                        return new Portal(
                            portalJson.roomId,
                            portalPosition
                        );
                    }).ToList();
                }

                DoorFactory doorFactory = new DoorFactory();

                if (connectionJson.doors != null && connectionJson.doors.Any())
                {
                    var decoratedDoor = doorFactory.CreateDecoratedDoor(connectionJson.doors);
                    connection.Door = decoratedDoor;

                    while (decoratedDoor is BaseDoorDecorator decorator)
                    {
                        if (decorator is IInventoryObserver inventoryObserver)
                        {
                            player.RegisterObserverInventory(inventoryObserver);
                        }

                        if (decorator is IHealthObserver healthObserver)
                        {
                            player.RegisterObserverHealth(healthObserver);
                        }

                        decoratedDoor = decorator._wrappee;
                    }

                    // Lastly, check the base door itself
                    if (decoratedDoor is IInventoryObserver baseObserver)
                    {
                        player.RegisterObserverInventory(baseObserver);
                    }

                    if (decoratedDoor is IHealthObserver baseHealthObserver)
                    {
                        player.RegisterObserverHealth(baseHealthObserver);
                    }
                }

                return connection;
            }).ToList();


            // Create and return the TempleOfDoomGame domain object
            return new TempleOfDoomGame(rooms, connections, player);
        }



        public void CheckGameStatus(Player player)
        {
            if (player.Lives <= 0)
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
