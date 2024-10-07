using TrafficLights.ColorSwitch;

namespace TrafficLights.TrafficLight
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public abstract class TrafficLightBase
    {
        protected List<TrafficLightBase> OtherTrafficLights;
        readonly public int ID;
        public readonly Direction Direction;
        public int QueueCount { get; set; }
        public bool IsMovmentAllowed { get => Color != Color.Red; }
        public Color Color { get => ColorSwitch.Color;}
        private ColorSwitchBase ColorSwitch {  get; set; }

        event MessageHandler QueueOverflow;
        delegate void MessageHandler(int id, int queueCount);

    }
}
