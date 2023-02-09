using Source.InputHandler;

namespace Source.UI
{
    public class ClickableTutorial : OneTimeTutorial, IClickable
    {
        public void Click() => 
            Complete();
    }
}