using System.Threading;
using TrafficLights.TrafficLight;
using TrafficLights.TrafficParticipants;

namespace TrafficLights
{
    public class Crossroad
    {
        public List<TrafficLightBase> TrafficLights = new List<TrafficLightBase>();
        public List<TrafficParticipantsBase> CrossingPartisipants = new List<TrafficParticipantsBase>();
        public int step = 0;

        public Crossroad()
        {
            InitializeTrafficLights();
        }

        public void Start(double stepDurationSeconds = 0, int stepsCount = 50)
        {
            for(int i = 0; i < stepsCount; i++)
            {
                step++;
                //await Task.Run(CycleStep);
                CycleStep();
                Thread.Sleep((int)(stepDurationSeconds * 1000));
            }
        }
        private void CycleStep()
        {
            CrossingPartisipants.Clear();
            UpdateNotify?.Invoke();
            CheckNotify?.Invoke();
            VehiclePassNotify?.Invoke();
            PedestrianPassNotify?.Invoke();

            CheckAccident();
            CrossingPartisipants.Clear();
            

            RandomTrafficGenerate(60,2);
        }
        public event Action UpdateNotify;
        public event Action CheckNotify;
        public event Action VehiclePassNotify;
        public event Action PedestrianPassNotify;

        private void InitializeTrafficLights()
        {
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Up, Direction.Left));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Up, Direction.Right));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Right, Direction.Up));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Right, Direction.Down));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Down, Direction.Right));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Down, Direction.Left));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Left, Direction.Down));
            TrafficLights.Add(new PedestrianTrafficLight(this, Direction.Left, Direction.Up));
            TrafficLights.Add(new VehicleTrafficLight(this, Direction.Up));
            TrafficLights.Add(new VehicleTrafficLight(this, Direction.Right));
            TrafficLights.Add(new VehicleTrafficLight(this, Direction.Down));
            TrafficLights.Add(new VehicleTrafficLight(this, Direction.Left));
            foreach(var light in TrafficLights)
            {
                UpdateNotify += light.OnUpdate;
                CheckNotify += light.OnCheck;
                VehiclePassNotify += light.OnVehiclePass;
                PedestrianPassNotify += light.OnPedestrianPass;
                foreach (var other in TrafficLights)
                {
                    if(other != light)
                    {
                        light.CompareRequest += other.ComparePriority;
                    }
                }
            }
        }
        private void RandomTrafficGenerate(int probability = 60, int spawnCountRange = 5)
        {
            if (probability < 0 || probability > 100)
                throw new Exception("probability must be in the range from 0 to 100.");
            if (spawnCountRange < 0)
                throw new Exception("spawnCountRange must be non-negative integer.");

            foreach (var light in TrafficLights)
            {
                if (new Random().Next(100) > probability)
                    light.QueueEncrease(new Random().Next(spawnCountRange));
            }
        }
        private void CheckAccident()
        {
            foreach (var participant in CrossingPartisipants)
            {
                foreach (var other in CrossingPartisipants)
                {
                    if (participant != other)
                    {
                        if (IsIntersect(participant,other))
                        {
                            throw new Exception($"An accident with {participant.GetType().Name} and {other.GetType().Name} on directions {participant.Direction} and {other.Direction}, on step {step}");
                        }
                    }
                }
                   
            }
        }
        public bool IsIntersect(TrafficParticipantsBase a, TrafficParticipantsBase b) 
        {
            bool result = true;
            switch (a)
            {
                case Vehicle:
                    switch (b)
                    {
                        case Vehicle:
                            result = ((int)a.Direction + (int)b.Direction) % 2 != 0;
                            break;
                        case Pedestrian:
                            var pedestrian = (Pedestrian)b;
                            if (a.Direction == Direction.Left)
                            {
                                result = pedestrian.RoadSide != Direction.Down;
                                break;
                            }
                            else if ((int)a.Direction - 1 == (int)pedestrian.RoadSide)
                            {
                                result = false;
                                break;
                            }
                            result = true;
                            break;
                    }
                    break;
                case Pedestrian:
                    switch (b)
                    {
                        case Vehicle:
                            if (b.Direction == Direction.Left)
                            {
                                result = ((Pedestrian)a).RoadSide != Direction.Down;
                                break;
                            }
                            else if ((int)b.Direction - 1 == (int)((Pedestrian)a).RoadSide)
                            {
                                result = false;
                                break;
                            }
                            result = true;
                            break;
                        case Pedestrian:
                            result = false;
                            break;
                    }
                    break;

            }
            return result;
        }
    }
}
