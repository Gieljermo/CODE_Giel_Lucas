
public class TempleOfDoomGameJson
{
    public RoomJson[] rooms { get; set; }
    public ConnectionJson[] connections { get; set; }
    public PlayerJson player { get; set; }
}

public class PlayerJson
{
    public int startRoomId { get; set; }
    public int startX { get; set; }
    public int startY { get; set; }
    public int lives { get; set; }
}

public class RoomJson
{
    public int id { get; set; }
    public string type { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public ItemJson[] items { get; set; }
}

public class ItemJson
{
    public string type { get; set; }
    public int damage { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public string color { get; set; }
}

public class ConnectionJson
{
    public int NORTH { get; set; }
    public int SOUTH { get; set; }
    public DoorJson[] doors { get; set; }
    public PortalJson[] portal { get; set; }
    public int WEST { get; set; }
    public int EAST { get; set; }
}

public class DoorJson
{
    public string type { get; set; }
    public string color { get; set; }
    public int no_of_stones { get; set; }
}

public class PortalJson
{
    public int roomId { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}
