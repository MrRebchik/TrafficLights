using TrafficLights.TrafficLight;

namespace TrafficLights.TrafficParticipants
{
    public class Pedestrian : TrafficParticipantsBase
    {
        public readonly Direction RoadSide;
        public Pedestrian(Direction dir, Direction roadSide): base(dir)
        {
            RoadSide = roadSide;
        }
    }
}
