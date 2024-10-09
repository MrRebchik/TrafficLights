namespace TrafficLights.TrafficLight
{
    public class VehicleTrafficLight : TrafficLightBase
    {
        public VehicleTrafficLight(Crossroad crossroad, Direction direction): base(crossroad, direction) { }

        protected override bool IsIntersect()
        {
            return false; // TODO
        }

        protected override bool CompareWaitingTimeSum(int sum, Direction direction)
        {
            if (GetQueueWaitingTimeSum() == sum)
            {
                return Direction > direction; // TODO для пешеходов по другому!!
            }
            else
                return GetQueueWaitingTimeSum() > sum;
        }
    }
}
