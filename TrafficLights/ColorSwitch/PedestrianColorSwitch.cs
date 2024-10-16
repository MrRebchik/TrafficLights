﻿using TrafficLights.TrafficLight;

namespace TrafficLights.ColorSwitch
{
    public class PedestrianColorSwitch : ColorSwitchBase
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
