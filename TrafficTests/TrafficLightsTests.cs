using TrafficLights.TrafficLight;
using TrafficLights.TrafficParticipants;
using TrafficLights;

namespace TrafficTests
{
    public class TrafficLightsTests
    {
        [Fact]
        public void VehicleTrafficLightIsIntersectMethod()
        {
            var cross = new Crossroad();
            var LeftVehicleLight = new VehicleTrafficLight(cross, Direction.Left);
            var RightVehicleLight = new VehicleTrafficLight(cross, Direction.Right);
            var UpVehicleLight = new VehicleTrafficLight(cross, Direction.Up);
            var DownLeftPedLight = new PedestrianTrafficLight(cross, Direction.Down, Direction.Left);
            var UpLeftPedLight = new PedestrianTrafficLight(cross, Direction.Up, Direction.Left);
            var LeftUpPedLight = new PedestrianTrafficLight(cross, Direction.Left, Direction.Up);
            var RightUpPedLight = new PedestrianTrafficLight(cross, Direction.Right, Direction.Up);

            Assert.True(RightUpPedLight.IsIntersect(LeftVehicleLight));
            Assert.True(LeftUpPedLight.IsIntersect(LeftVehicleLight));
            Assert.True(UpVehicleLight.IsIntersect(LeftVehicleLight));
            Assert.True(LeftVehicleLight.IsIntersect(DownLeftPedLight));

            Assert.False(LeftVehicleLight.IsIntersect(RightVehicleLight));
            Assert.False(LeftUpPedLight.IsIntersect(RightUpPedLight));
            Assert.False(LeftUpPedLight.IsIntersect(UpLeftPedLight));
        }
    }
}
