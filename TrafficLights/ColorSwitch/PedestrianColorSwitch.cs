using TrafficLights.TrafficLight;

namespace TrafficLights.ColorSwitch
{
    internal class PedestrianColorSwitch : ColorSwitchBase
    {
        public override void NextColor()
        {
            if (Color == Color.Red)
            {
                Color = Color.Green;
                PreviousColor = Color.Red;
            }
            else
            {
                Color = Color.Red;
                PreviousColor = Color.Green;
            }
        }
    }
}
