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

using CODE_TempleOfDoom_DownloadableContent;

namespace TempleOfDoom.controller
{
    public class GameController
    {
        public TempleOfDoomGame TempleOfDoomGame { get; set; }
        private bool gameRunning = true;
        private BoardController boardController;
        private PlayerController playerController;
        private FieldController fieldController;
        private readonly TempleOfDoomGameJson _gameData;

        public GameController(IFileReader fileReader, string fileName)
        {
            this._gameData = fileReader.readFile(fileName);
            this.TempleOfDoomGame = GenerateGameClasses(_gameData);
            fieldController = new FieldController();
            this.boardController = new BoardController(this, fieldController);
            this.playerController = new PlayerController(TempleOfDoomGame.Player, boardController);


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
                playerController.Action(key, boardController._gameRoom);
            }
        }


        public TempleOfDoomGame GenerateGameClasses(TempleOfDoomGameJson data)
        {
            Player player = new Player(
            data.player.startRoomId,
            data.player.startX,
            data.player.startY,
            data.player.lives
);

            List<Room> rooms = data.rooms.Select(roomJson =>
            {
                var room = new Room(
                    roomJson.id,
                    roomJson.type,
                    roomJson.width,
                    roomJson.height
                );

                ItemFactory itemFactory = new ItemFactory();
                if (roomJson.items != null)
                {
                    room.Items = roomJson.items.Select(itemJson => itemFactory.CreateItem(itemJson)).ToList();
                }

                EnemyFactory enemyFactory = new EnemyFactory();
                if (roomJson.enemies != null)
                {
                    room.Enemies = roomJson.enemies.Select(enemyJson => enemyFactory.CreateEnemy(enemyJson)).ToList();
                    foreach(var enemy in room.Enemies)
                    {
                        if(enemy is IMovementObserver observer)
                        {
                            player.AddMovementObserver(observer);
                        }    
                    }
                }

                if (roomJson.specialFloorTiles != null)
                {
                    room.SpecialFloorTiles = roomJson.specialFloorTiles?
                        .Select(tile => new SpecialTile
                        {
                            type = tile.type,
                            x = tile.x,
                            y = tile.y
                        })
                        .ToList();
                }

                return room;
            }).ToList();


            // Convert connections
            List<Connection> connections = data.connections.Select(connectionJson =>
            {
                var ladder = (connectionJson.UPPER != 0 && connectionJson.LOWER != 0)
                    ? new Ladder(connectionJson.ladder.upperX, connectionJson.ladder.upperY, connectionJson.ladder.lowerX, connectionJson.ladder.lowerY)
                    : null;

                var connection = new Connection(
                    connectionJson.NORTH,
                    connectionJson.WEST,
                    connectionJson.SOUTH,
                    connectionJson.EAST,
                    connectionJson.UPPER,
                    connectionJson.LOWER,
                    ladder
                );

                DoorFactory doorFactory = new DoorFactory();

                if (connectionJson.doors != null && connectionJson.doors.Any())
                {
                    var decoratedDoor = doorFactory.CreateDecoratedDoor(connectionJson.doors);
                    connection.Door = decoratedDoor;

                    while (decoratedDoor is BaseDoorDecorator decorator)
                    {
                        if (decorator is IInventoryObserver inventoryObserver)
                        {
                            player.AddInventoryObserver(inventoryObserver);
                        }

                        if (decorator is IHealthObserver healthObserver)
                        {
                            player.AddHealthObserver(healthObserver);
                        }

                        decoratedDoor = decorator._wrappee;
                    }

                    if (decoratedDoor is IInventoryObserver baseObserver)
                    {
                        player.AddInventoryObserver(baseObserver);
                    }

                    if (decoratedDoor is IHealthObserver baseHealthObserver)
                    {
                        player.AddHealthObserver(baseHealthObserver);
                    }
                }

                return connection;
            }).ToList();


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
