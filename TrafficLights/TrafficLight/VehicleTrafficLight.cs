using TrafficLights.TrafficParticipants;
using TrafficLights.ColorSwitch;

namespace TrafficLights.TrafficLight
{
    public class VehicleTrafficLight : TrafficLightBase
    {
        public VehicleTrafficLight(Crossroad crossroad, Direction direction): base(crossroad, direction) 
        {
            ColorSwitch = new VehicleColorSwitch();
        }

        public override bool IsMovmentAllowed { get => Color == Color.Green || (Color == Color.Yellow && ColorSwitch.PreviousColor == Color.Green); }
        public override void QueueEncrease(int count)
        {
            for (int i = 0; i < count; i++)
                Queue.Enqueue(new Vehicle(Direction));
        }
        public override bool IsIntersect(TrafficLightBase light)
        {
            bool result = false;
            switch (light)
            {
                case VehicleTrafficLight:
                    result = ((int)Direction+(int)light.Direction % 2 != 0);
                    break;
                case PedestrianTrafficLight: // ПРОВЕРИТЬ ВСЁ ЭТО todo
                    var pedestrianLight = (PedestrianTrafficLight) light;
                    if (Direction == Direction.Left)
                    {
                        result = pedestrianLight.RoadSide != Direction.Down;
                        break;
                    }
                    else if ((int)Direction - 1 == (int)pedestrianLight.RoadSide)
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
