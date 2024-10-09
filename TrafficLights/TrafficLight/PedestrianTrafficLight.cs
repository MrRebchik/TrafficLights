using System.Collections.Generic;
using TrafficLights.TrafficParticipants;

namespace TrafficLights.TrafficLight
{
    public class PedestrianTrafficLight : TrafficLightBase
    {
        public readonly Direction RoadSide;
        public PedestrianTrafficLight(Crossroad crossroad, Direction direction, Direction roadSide): base(crossroad, direction)
        {
            RoadSide = roadSide;
        }

        public override void QueueEncrease(int count)
        {
            for (int i = 0; i < count; i++)
                Queue.Enqueue(new Pedestrian(Direction, RoadSide));
        }
    }
}
