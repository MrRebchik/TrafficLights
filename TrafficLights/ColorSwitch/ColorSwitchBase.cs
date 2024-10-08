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
        public Color Color { get; protected set; }
        public Color PreviousColor { get; protected set; }

        public virtual void NextColor()
        {

        }
    }
}
