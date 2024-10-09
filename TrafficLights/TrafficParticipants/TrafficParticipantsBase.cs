using TrafficLights.TrafficLight;

namespace TrafficLights.TrafficParticipants
{
    public abstract class TrafficParticipantsBase
    {
        public readonly Direction Direction;
        public int WaitingTime {  get; private set; }
        public TrafficParticipantsBase(Direction direction)
        {
            this.Direction = direction;
        }
        public void EncreaseWatingTime()
        {
            WaitingTime++;
        }
    }
}
