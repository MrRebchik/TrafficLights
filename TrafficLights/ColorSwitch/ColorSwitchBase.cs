namespace TrafficLights.ColorSwitch
{
    public enum Color
    {
        Green,
        Yellow,
        Red
    }
    public class ColorSwitchBase
    {
        public ColorSwitchBase()
        {
            Color = Color.Red;
        }
        public Color Color { get; protected set; }
        public Color PreviousColor { get; protected set; }
        public void SolveInput(bool isGreenNeeded, bool isMovmentAllowed, bool isAnyoneYellow = false)
        {
            if(Color == Color.Yellow)
            {
                NextColor();
                return;
            }
            if (isAnyoneYellow)
            {
                return;
            }
            if (isGreenNeeded != isMovmentAllowed)
                NextColor();
        }

        public virtual void NextColor()
        {

        }
    }
}
