namespace TempleOfDoom.model.Interfaces
{
    public interface IEnemy
    {
        int X { get; }
        int Y { get; }
        void Move();
        bool isDead();
    }
}