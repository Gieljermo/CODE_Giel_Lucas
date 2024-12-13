namespace TempleOfDoom.model.Observers
{
    public interface IHealthObserver
    {
        public void OnHealthChanged(int amountOfLives);
    }
}