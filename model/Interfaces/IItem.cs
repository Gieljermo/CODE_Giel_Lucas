using TempleOfDoom.model;

namespace Domain.Interfaces
{
    public interface IItem
    {

        string Type { get; set; }
        void Interact(Player player, Field field, Room room);
    }
}