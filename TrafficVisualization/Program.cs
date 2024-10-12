using TrafficLights;
using System.Timers;

namespace TrafficVisualization
{
    internal class Program
    {
        static double timeScale_s = 0.01;
        private static System.Timers.Timer aTimer;
        static Crossroad cross;
        static TrafficStatistics stats;
        static void Main(string[] args)
        {
            cross = new Crossroad();
            stats = new TrafficStatistics(cross);
            cross.Start(timeScale_s,100);
            SetTimer();
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
            int sum = 0;
            foreach(var l in cross.TrafficLights)
            {
                sum += l.PassedCount;
            }
            Console.WriteLine(sum.ToString());
        }
        static void ConsoleVisualize(Object source, ElapsedEventArgs e)
        {
            var crs = cross;

            Console.Clear();

            Console.WriteLine($"\t.\t{crs.TrafficLights[0].QueueCount}||.\t.\t|.\t.\t||{crs.TrafficLights[1].QueueCount}. \t.\t");
            Console.WriteLine($".\t .{crs.TrafficLights[7].QueueCount}.\t||\t{crs.TrafficLights[8].QueueCount}\t|.\t.\t||.\t{crs.TrafficLights[2].QueueCount}.\t");
            Console.WriteLine($"==================.\t.\t|.\t.\t=================");
            Console.WriteLine($".\t .\t..\t\t|.\t.\t.\t{crs.TrafficLights[9].QueueCount}.\t");
            Console.WriteLine($"--------------------------------+-------------------------------");
            Console.WriteLine($".\t .{crs.TrafficLights[11].QueueCount}.\t\t\t|.\t.\t.\t.\t");
            Console.WriteLine($"==================.\t.\t|.\t.\t=================");
            Console.WriteLine($".\t .{crs.TrafficLights[6].QueueCount}.\t||\t\t|.\t{crs.TrafficLights[10].QueueCount}.\t||.\t{crs.TrafficLights[3].QueueCount}.\t");
            Console.WriteLine($"\t.\t{crs.TrafficLights[5].QueueCount}||.\t.\t|.\t.\t||{crs.TrafficLights[4].QueueCount}. \t.\t");
            Console.WriteLine($"################################################################");

            foreach( var l in crs.TrafficLights)
                if(l.IsMovmentAllowed)
                    Console.WriteLine(l.QueueCount.ToString() + " Direction: " + l.Direction.ToString() + "; Color: " + l.Color.ToString());

        }
        private static void SetTimer()
        {
            aTimer = new System.Timers.Timer(timeScale_s * 1000);
            aTimer.Elapsed += ConsoleVisualize;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
    }
}
