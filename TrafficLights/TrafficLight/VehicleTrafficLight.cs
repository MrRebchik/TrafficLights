using TrafficLights.TrafficParticipants;

namespace TrafficLights.TrafficLight
{
    public class VehicleTrafficLight : TrafficLightBase
    {
        public VehicleTrafficLight(Crossroad crossroad, Direction direction): base(crossroad, direction) { }

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
