using TempleOfDoom.model;

namespace TempleOfDoom.controller
{
    public class GameController
    {
        public TempleOfDoomGame TempleOfDoomGame {get; set;}

        public GameController(TempleOfDoomGameJson data)
        {
            this.TempleOfDoomGame = GenerateGameClasses(data);
        }

        public TempleOfDoomGame GenerateGameClasses(TempleOfDoomGameJson data)
        {
            List<Room> rooms = data.rooms.Select(roomJson => new Room(
                   roomJson.id,
                   roomJson.type,
                   roomJson.width,
                   roomJson.height,
                   roomJson.items?.Select(itemJson => new Item(
                       itemJson.type,
                       itemJson.damage,
                       itemJson.x,
                       itemJson.y,
                       itemJson.color
                   )).ToList()
               )).ToList();

            // Convert connections
            List<Connection> connections = data.connections.Select(connectionJson => new Connection(
                connectionJson.NORTH,
                connectionJson.SOUTH,
                connectionJson.WEST,
                connectionJson.EAST,
                connectionJson.doors.Select(doorJson => new Door(
                    doorJson.type,
                    doorJson.color,
                    doorJson.no_of_stones
                )).ToList()
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
    }
}
