using System.Collections.Generic;
using Source.UI;

namespace Source.Tutorials
{
    public class TutorialRunner : ITutorialRunner
    {
        private readonly Queue<ITutorial> _tutorialsQueue = new();
        private ITutorial _tutorial;

        public void Update()
        {
            if (!_tutorial.Completed)
                return;

            _tutorial.Finish();
            _tutorial = _tutorialsQueue.Dequeue();
            _tutorial.StartTutorial();
        }

        public void Run(ITutorial tutorial)
        {
            if (_tutorial != null)
            {
                _tutorial.Stop();
                _tutorialsQueue.Enqueue(_tutorial);
            }

            _tutorial = tutorial;
            _tutorial.StartTutorial();
        }
    }
}
