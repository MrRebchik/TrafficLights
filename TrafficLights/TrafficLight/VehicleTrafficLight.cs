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
        protected override bool IsIntersect(TrafficLightBase trafficLight)
        {
            return false; // TODO
        }
    }
}
