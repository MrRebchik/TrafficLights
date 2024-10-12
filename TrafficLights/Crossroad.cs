using System.Threading;
using TrafficLights.TrafficLight;

namespace TrafficLights
{
    public class Crossroad
    {
        public List<TrafficLightBase> TrafficLights = new List<TrafficLightBase>();

        public Crossroad()
        {
            InitializeTrafficLights();
        }

        public async void Start(double stepDurationSeconds = 0, int stepsCount = 50)
        {
            for(int i = 0; i < stepsCount; i++)
            {
                await Task.Run(() => CycleStep());
                Thread.Sleep((int)(stepDurationSeconds * 1000));
            }
        }
        private void CycleStep()
        {
            UpdateNotify?.Invoke();
            CheckNotify?.Invoke();

            RandomTrafficGenerate();
        }
        public event Action UpdateNotify;
        public event Action CheckNotify;

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
                foreach(var other in TrafficLights)
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
    }
}
