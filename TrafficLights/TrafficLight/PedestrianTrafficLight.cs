using System.Collections.Generic;
using TrafficLights.ColorSwitch;
using TrafficLights.TrafficParticipants;

namespace TrafficLights.TrafficLight
{
    public class PedestrianTrafficLight : TrafficLightBase
    {
        public readonly Direction RoadSide;
        public PedestrianTrafficLight(Crossroad crossroad, Direction direction, Direction roadSide): base(crossroad, direction)
        {
            ColorSwitch = new PedestrianColorSwitch();
            RoadSide = roadSide;
        }

        public override void QueueEncrease(int count)
        {
            for (int i = 0; i < count; i++)
                Queue.Enqueue(new Pedestrian(Direction, RoadSide));
        }

        public override bool IsIntersect(TrafficLightBase light)
        {
            bool result = false;
            switch (light)
            {
                case PedestrianTrafficLight:
                    result = false;
                    break;
                case VehicleTrafficLight:
                    if (light.Direction == Direction.Left)
                    {
                        result = RoadSide != Direction.Down;
                        break;
                    }
                    else if ((int)light.Direction - 1 == (int)RoadSide)
                    {
                        result = false;
                        break;
                    }
                    result = true;
                    break;
            }
            return result;
        }
    }
}
