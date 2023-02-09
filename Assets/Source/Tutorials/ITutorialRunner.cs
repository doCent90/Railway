using Source.UI;

namespace Source.Tutorials
{
    public interface ITutorialRunner
    {
        void Update();
        void Run(ITutorial tutorial);
    }
}
