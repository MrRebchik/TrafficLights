namespace TrafficLights
{
    public class TrafficStatistics
    {
        public int StepsCount { get; private set; } = 0;
        public int? MaxWaitingTime { get; private set; }
        public double? AwarageMaxWaitingTime { get => MaxWaitingTime/ StepsCount;}
        public int PassedParticipantsAmount { get; private set; }
        Crossroad crossroad;
        public TrafficStatistics(Crossroad crossroad)
        {
            this.crossroad = crossroad;
            crossroad.UpdateNotify += UpdateStatistics;
        }

        void UpdateStatistics()
        {
            StepsCount++;
            foreach(var l in crossroad.TrafficLights)
            {
                if (MaxWaitingTime < l.MaxQueueWaitingTime) MaxWaitingTime = l.MaxQueueWaitingTime;
            }
        }
    }
}
