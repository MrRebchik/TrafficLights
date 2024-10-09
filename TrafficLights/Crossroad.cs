using TrafficLights.TrafficLight;

namespace TrafficLights
{
    public class Crossroad
    {
        public List<TrafficLightBase> TrafficLights = new List<TrafficLightBase>();

        public Crossroad()
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
            }
        }

        public void Start()
        {
            while(true) // Задать необходимое количество итераций
            {
                CycleStep();
            }
        }
        private void CycleStep()
        {
            UpdateNotify?.Invoke();
            CheckNotify?.Invoke();

            foreach (var light in TrafficLights)
            {
                if(new Random().Next(100)>60)
                    light.QueueEncrease(new Random().Next(2));
            }

        }
        event Action UpdateNotify;
        event Action CheckNotify;
    }
}
