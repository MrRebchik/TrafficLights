using TrafficLights;
using TrafficLights.ColorSwitch;
using TrafficLights.TrafficLight;
using TrafficLights.TrafficParticipants;

namespace TrafficTests
{
    public class ColorSwitchTests
    {
        [Fact]
        public void VehicleColorSwitchNextMethod()
        {
            VehicleColorSwitch colorSwitch = new VehicleColorSwitch();
            colorSwitch.NextColor();
            Assert.Equal(Color.Yellow, colorSwitch.Color);
            Assert.Equal(Color.Red, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
            Assert.Equal(Color.Green, colorSwitch.Color);
            Assert.Equal(Color.Yellow, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
            Assert.Equal(Color.Yellow, colorSwitch.Color);
            Assert.Equal(Color.Green, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
            Assert.Equal(Color.Red, colorSwitch.Color);
            Assert.Equal(Color.Yellow, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
            Assert.Equal(Color.Yellow, colorSwitch.Color);
            Assert.Equal(Color.Red, colorSwitch.PreviousColor);
        }
        [Fact]
        public void PedestrianColorSwitchNextMethod()
        {
            PedestrianColorSwitch colorSwitch = new PedestrianColorSwitch();
            colorSwitch.NextColor();
            Assert.Equal(Color.Green, colorSwitch.Color);
            Assert.Equal(Color.Red, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
            Assert.Equal(Color.Red, colorSwitch.Color);
            Assert.Equal(Color.Green, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
            Assert.Equal(Color.Green, colorSwitch.Color);
            Assert.Equal(Color.Red, colorSwitch.PreviousColor);
            colorSwitch.NextColor();
        }
    }
}