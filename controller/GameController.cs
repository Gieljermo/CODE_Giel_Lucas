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
        private const int MAX_AMOUNT_OF_STONES = 5;
        public TempleOfDoomGame TempleOfDoomGame { get; set; }
        private bool gameRunning = true;
        private BoardController boardController;
        private PlayerController playerController;
        private OpponentController opponentController;
        private FieldController fieldController;
        private readonly TempleOfDoomGameJson _gameData;

        public GameController(IFileReader fileReader, string fileName)
        {
            _gameData = fileReader.readFile(fileName);
            TempleOfDoomGame = GenerateGameClasses(_gameData);

            InitializeObservers();

            fieldController = new FieldController();
            boardController = new BoardController(this, fieldController);
            playerController = new PlayerController(TempleOfDoomGame.Player, boardController);
            opponentController = new OpponentController(boardController);

            InitializeStartingRoom();
            Loop();
        }

        private void InitializeStartingRoom()
        {
            var startingRoom = TempleOfDoomGame.Rooms.FirstOrDefault(r => r.Id == TempleOfDoomGame.Player.StartRoomId);
            if (startingRoom != null)
            {
                boardController.CreateStartingRoom(TempleOfDoomGame.Player, startingRoom);
            }
        }

        private void Loop()
        {
            while (gameRunning)
            {
                boardController.DrawRoom();
                ConsoleKey key = Console.ReadKey(true).Key;

                ProcessPlayerAndOpponentMove(key);
                CheckPlayerAndOpponentDamage(key);
            }
        }

        private void ProcessPlayerAndOpponentMove(ConsoleKey key)
        {
            playerController.Move(key, boardController.CurrentRoom);
            opponentController.Move(key, boardController.CurrentRoom);
        }

        private void CheckPlayerAndOpponentDamage(ConsoleKey key)
        {
            playerController.CheckDamage(boardController.CurrentRoom);
            opponentController.CheckDamage(key, boardController.CurrentRoom, TempleOfDoomGame.Player);
        }

        public TempleOfDoomGame GenerateGameClasses(TempleOfDoomGameJson data)
        {
            List<Room> rooms = GenerateRooms(data.rooms);
            Player player = GeneratePlayer(data.player);
            List<Connection> connections = GenerateConnections(data.connections);

            return new TempleOfDoomGame(rooms, connections, player);
        }

        private void InitializeObservers()
        {
            foreach (var connection in TempleOfDoomGame.Connections)
            {
                RegisterDoorObservers(connection.Doors);
            }
        }

        private List<Room> GenerateRooms(IEnumerable<RoomJson> roomData)
        {
            return roomData.Select(roomJson =>
            {
                var room = new Room(roomJson.id, roomJson.type, roomJson.width, roomJson.height);
                room.Items = GenerateItems(roomJson.items);
                room.SpecialFloorTiles = GenerateSpecialFloorTiles(roomJson.specialFloorTiles);
                room.Opponents = GenerateOpponents(roomJson.enemies);

                return room;
            }).ToList();
        }

        private List<Item> GenerateItems(IEnumerable<ItemJson> itemData)
        {
            var itemFactory = new FieldElementFactory();

            if (itemData == null)
            {
                return new List<Item>();
            }

            return itemData.Select(itemJson =>
            {
                IPosition position = new Position(itemJson.x, itemJson.y);
                return (Item)itemFactory.CreateItem(itemJson.type, position, itemJson.damage, itemJson.color);
            }).ToList();
        }

        private List<SpecialFloorTile> GenerateSpecialFloorTiles(SpecialFloorTileJson[] floorData)
        {
            if (floorData == null)
            {
                return new List<SpecialFloorTile>();
            }

            return floorData.Select(floorJson =>
            {
                bool isDirectionValid = Enum.TryParse<Direction>(floorJson.direction, true, out var direction);

                if (isDirectionValid)
                {
                    IPosition position = new Position(floorJson.x, floorJson.y);
                    return new SpecialFloorTile(floorJson.type, position, direction);
                }
                else
                {
                    throw new ArgumentException($"Invalid direction: {floorJson.direction}");
                }
            }).ToList();
        }

        private List<Opponent> GenerateOpponents(IEnumerable<EnemyJson> enemyData)
        {
            if (enemyData == null)
            {
                return new List<Opponent>();
            }

            return enemyData.Select(opponentJson =>
            {
                IPosition position = new Position(opponentJson.x, opponentJson.y);
                return new Opponent(opponentJson.type, position, opponentJson.minX, opponentJson.maxX, opponentJson.minY, opponentJson.maxY);
            }).ToList();
        }

        private Player GeneratePlayer(PlayerJson playerJson)
        {
            return new Player(playerJson.startRoomId, playerJson.startX, playerJson.startY, playerJson.lives);
        }

        private List<Connection> GenerateConnections(IEnumerable<ConnectionJson> connectionData)
        {
            return connectionData.Select(connectionJson =>
            {
                var connection = new Connection(connectionJson.NORTH, connectionJson.WEST, connectionJson.SOUTH, connectionJson.EAST);
                connection.Portals = GeneratePortals(connectionJson.portal);
                connection.Doors = GenerateDoors(connectionJson.doors);

                return connection;
            }).ToList();
        }

        private List<Portal> GeneratePortals(IEnumerable<PortalJson> portalData)
        {
            if (portalData == null)
            {
                return new List<Portal>();
            }

            return portalData.Select(portalJson =>
            {
                IPosition portalPosition = new Position(portalJson.x, portalJson.y);
                return new Portal(portalJson.roomId, portalPosition);
            }).ToList();
        }

        private List<IDoor> GenerateDoors(IEnumerable<DoorJson> doorData)
        {
            var doorFactory = new DoorFactory();
            var decoratedDoors = new List<IDoor>();

            if (doorData != null && doorData.Any())
            {
                int index = 0;
                var doorList = doorData.ToList();

                while (index < doorList.Count)
                {
                    var doorJson = doorList[index];
                    var decoratedDoor = doorFactory.CreateDecoratedDoor(new List<DoorJson> { doorJson });
                    decoratedDoors.Add(decoratedDoor);
                    index++;
                }
            }

            return decoratedDoors;
        }

        private void RegisterDoorObservers(IEnumerable<IDoor> doors)
        {
            foreach (var door in doors)
            {
                var currentDoor = door;

                while (currentDoor is BaseDoorDecorator decorator)
                {
                    RegisterObserver(decorator);

                    currentDoor = decorator._wrappee;
                }

                RegisterObserver(currentDoor);
            }
        }

        private void RegisterObserver(IDoor door)
        {
            if (TempleOfDoomGame == null)
            {
                return;
            }

            if (door is IInventoryObserver inventoryObserver)
            {
                TempleOfDoomGame.Player.RegisterObserverInventory(inventoryObserver);
            }

            if (door is IHealthObserver healthObserver)
            {
                TempleOfDoomGame.Player.RegisterObserverHealth(healthObserver);
            }
        }

        public void CheckGameStatus(Player player)
        {
            if (player.Lives <= 0)
            {
                EndGame(false);
            }
            else if (player.AmountOfStones >= MAX_AMOUNT_OF_STONES)
            {
                EndGame(true);
            }
        }

        private void EndGame(bool playerWon)
        {
            gameRunning = false;

            if (playerWon)
            {
                boardController.DrawWinScreen();
            }
            else
            {
                boardController.DrawLosingScreen();
            }
        }
    }
}
