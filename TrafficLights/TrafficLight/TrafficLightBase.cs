using System.Collections;
using TrafficLights.ColorSwitch;
using TrafficLights.TrafficParticipants;

namespace TrafficLights.TrafficLight
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public abstract class TrafficLightBase
    {
        protected readonly List<TrafficLightBase> OtherTrafficLights;
        protected Queue<TrafficParticipantsBase> Queue;
        public readonly int ID;
        public readonly Direction Direction;
        public int QueueCount { get => Queue.Count; }
        public int WaitingTime { get; set; }
        public virtual bool IsMovmentAllowed { get => Color != Color.Red; }
        public Color Color { get => ColorSwitch.Color;}
        protected ColorSwitchBase ColorSwitch {  get; set; }
        public int Priority { get; set; }

        event MessageHandler QueueOverflow;
        delegate void MessageHandler(int id, int queueCount, int queueWaitingTimeSum, int priority);

        public virtual void QueueEncrease(int count = 1) { }
        public virtual void QueueDecrease()
        {
            Queue.Dequeue();
        }
        private int GetQueueWaitingTimeSum()
        {
            int sum = 0;
            foreach(var p in Queue)
            {
                sum += p.WaitingTime;
            }
            return sum;
        }
        private int GetOthersQueuesSum()
        {
            int sum = 0;
            foreach(TrafficLightBase t in OtherTrafficLights)
            {
                if (t.ID != ID) sum += t.QueueCount;
            }
            return sum;
        }
    }
}
