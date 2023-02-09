
namespace Source.Characters.Upgrades
{
    public abstract class LevelProgression<T>
    {
        public abstract T ForLevel(int currentLevel);
    }
}