using TrafficLights;
using TrafficLights.TrafficLight;
using TrafficLights.TrafficParticipants;

namespace TrafficTests
{
    public class CrossroadTests
    {
        [Fact]
        public void IsIntersectMethod()
        {
            var cross = new Crossroad();
            var LeftVehicle = new Vehicle(Direction.Left);
            var RightVehicle = new Vehicle(Direction.Right);
            var UpVehicle = new Vehicle(Direction.Up);
            var DownLeftPed = new Pedestrian(Direction.Down, Direction.Left);
            var UpLeftPed = new Pedestrian(Direction.Up, Direction.Left);
            var LeftUpPed = new Pedestrian(Direction.Left, Direction.Up);
            var RightUpPed = new Pedestrian(Direction.Right, Direction.Up);

            Assert.True(cross.IsIntersect(RightUpPed,LeftVehicle));
            Assert.True(cross.IsIntersect(LeftUpPed, LeftVehicle));
            Assert.True(cross.IsIntersect(UpVehicle, LeftVehicle));
            Assert.True(cross.IsIntersect(LeftVehicle, DownLeftPed));

            Assert.False(cross.IsIntersect(LeftVehicle, RightVehicle));
            Assert.False(cross.IsIntersect(LeftUpPed, RightUpPed));
            Assert.False(cross.IsIntersect(LeftUpPed, UpLeftPed));
        }
    }
}
