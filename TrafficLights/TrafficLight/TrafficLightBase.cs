using System.Collections;
using TrafficLights.ColorSwitch;
using TrafficLights.TrafficParticipants;

namespace TrafficLights.TrafficLight
{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
    public abstract class TrafficLightBase
    {
        protected List<TrafficParticipantsBase> CrossingPartisipants;
        protected readonly List<TrafficLightBase> OtherTrafficLights;
        protected Queue<TrafficParticipantsBase> Queue = new Queue<TrafficParticipantsBase>();
        public readonly int ID;
        public readonly Direction Direction;
        protected bool IsGreenNeeded;
        public int Priority { get; set; }
        public int QueueCount { get => Queue.Count; }
        public int MaxQueueWaitingTime { get { if (Queue.Count > 0) return Queue.FirstOrDefault().WaitingTime; else return 0; }  }
        public int WaitingTime { get; set; }
        public virtual bool IsMovmentAllowed { get => Color != Color.Red; }
        protected ColorSwitchBase ColorSwitch {  get; set; }
        public Color Color { get => ColorSwitch.Color;}
        public int PassedCount { get; set; }

        public event MessageHandler CompareRequest;
        public delegate void MessageHandler(int id, int queueCount, int queueWaitingTimeSum);

        public TrafficLightBase(Crossroad crossroad, Direction direction)
        {
            CrossingPartisipants = crossroad.CrossingPartisipants;
            OtherTrafficLights = crossroad.TrafficLights;
            Direction = direction;
            ID = OtherTrafficLights.Count + 1;
            SetDefaultFlagsValues();
        }

        public virtual void QueueEncrease(int count = 1) { }
        public virtual void QueueDecrease()
        {
            if(Queue.Count != 0)
            {
                CrossingPartisipants.Add(Queue.Dequeue());
                PassedCount++;
            }
            WaitingTime = 0;
        }
        public void OnUpdate()
        {
            WaitingTime++;
            CompareRequest?.Invoke(ID, QueueCount, GetQueueWaitingTimeSum());
        }
        public void OnCheck()
        {
            foreach (var other in OtherTrafficLights.Where(x => x.IsGreenNeeded))
            {
                if (this != other)
                {
                    if (IsIntersect(other))
                    {
                        if (Priority > other.Priority)
                            other.IsGreenNeeded = false;
                        else
                            IsGreenNeeded = false;
                    }
                }
            }
        }
        public void OnVehiclePass()
        {
            if (GetType() == typeof(VehicleTrafficLight))
                ColorSwitch.SolveInput(IsGreenNeeded, IsMovmentAllowed);
            
        }
        public void OnPedestrianPass()
        {
            if (GetType() == typeof(PedestrianTrafficLight))
            {
                if (IsGreenNeeded)
                {
                    foreach(var vLights in OtherTrafficLights.Where(x => x.GetType() == typeof(VehicleTrafficLight)).Where(x => x.IsMovmentAllowed))
                    {
                        if (IsIntersect(vLights)) IsGreenNeeded = false;
                    }
                }
                ColorSwitch.SolveInput(IsGreenNeeded, IsMovmentAllowed);
            }

            if (IsMovmentAllowed)
                QueueDecrease();
            SetDefaultFlagsValues();
            foreach (var participants in Queue)
                participants.EncreaseWatingTime();
        }
        public abstract bool IsIntersect(TrafficLightBase light); 
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
                if(IsIntersect(OtherTrafficLights.FirstOrDefault(x => x.ID == id)))IsGreenNeeded = false;
                Priority--;
            }
        }
        private void SetDefaultFlagsValues()
        {
            IsGreenNeeded = true;
            Priority = OtherTrafficLights.Count;
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
        protected int GetOthersQueuesSum() // Еще никак не использовалось
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
