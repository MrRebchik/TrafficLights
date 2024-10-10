using System.Collections;
using TrafficLights.ColorSwitch;
using TrafficLights.TrafficParticipants;

namespace TrafficLights.TrafficLight
{
    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }
    public abstract class TrafficLightBase
    {
        protected readonly List<TrafficLightBase> OtherTrafficLights;
        protected Queue<TrafficParticipantsBase> Queue = new Queue<TrafficParticipantsBase>();
        public readonly int ID;
        public readonly Direction Direction;
        protected bool IsGreenNeeded;
        public int QueueCount { get => Queue.Count; }
        public int WaitingTime { get; set; }
        public virtual bool IsMovmentAllowed { get => Color != Color.Red; }
        public Color Color { get => ColorSwitch.Color;}
        protected ColorSwitchBase ColorSwitch {  get; set; }
        public int PassedCount { get; set; }

        public event MessageHandler CompareRequest;
        public delegate void MessageHandler(int id, int queueCount, int queueWaitingTimeSum);

        public TrafficLightBase(Crossroad crossroad, Direction direction)
        {
            IsGreenNeeded = true;
            OtherTrafficLights = crossroad.TrafficLights;
            Direction = direction;
            ID = OtherTrafficLights.Count + 1;
        }

        public virtual void QueueEncrease(int count = 1) { }
        public virtual void QueueDecrease()
        {
            Queue.Dequeue();
            PassedCount++;
        }
        public void OnUpdate()
        {
            CompareRequest?.Invoke(ID, QueueCount, GetQueueWaitingTimeSum());
        }
        public void OnCheck()
        {
            if (IsGreenNeeded)
                QueueDecrease();
            IsGreenNeeded = true;
            foreach(var participants in Queue)
            {
                participants.EncreaseWatingTime();
            }
        }
        protected abstract bool IsIntersect(TrafficLightBase light);
        public virtual void ComparePriority(int id, int queueCount, int queueWaitingTimeSum)
        {
            bool result;
            if (GetQueueWaitingTimeSum() == queueWaitingTimeSum)
            {
                result = Direction > OtherTrafficLights.FirstOrDefault(x => x.ID == id).Direction;
            }
            else
                result = GetQueueWaitingTimeSum() > queueWaitingTimeSum;
            if (!result)
            {
                IsGreenNeeded = false;
            }
        }
        protected int GetQueueWaitingTimeSum()
        {
            int sum = 0;
            foreach(var p in Queue)
            {
                sum += p.WaitingTime;
            }
            return sum;
        }
        protected int GetOthersQueuesSum()
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
