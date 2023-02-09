using Source.UI;

namespace Source.Map.ChunksLoader
{
    public class NullTutorial : ITutorial
    {
        public bool Completed { get; private set; }

        public void Stop()
        {
        }

        public void StartTutorial()
        {
            Completed = true;
        }

        public void Finish()
        {

        }
    }
}
