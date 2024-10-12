namespace TrafficLights.ColorSwitch
{
    public class VehicleColorSwitch : ColorSwitchBase
    {
        public override void NextColor()
        {
            if(Color == Color.Yellow)
            {
                if(PreviousColor == Color.Red)
                {
                    Color = Color.Green;
                    PreviousColor = Color.Yellow;
                }
                else
                {
                    Color = Color.Red;
                    PreviousColor = Color.Yellow;
                }
            }
            else if(Color == Color.Red)
            {
                Color = Color.Yellow;
                PreviousColor = Color.Red;
            }
            else
            {
                Color = Color.Yellow;
                PreviousColor = Color.Green;
            }
        }
    }
}
