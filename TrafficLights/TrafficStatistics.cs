namespace TrafficLights
{
    public class TrafficStatistics
    {
        public int StepsCount { get; private set; } = 1;
        public int MaxWaitingTime { get; private set; }
        public double AwarageMaxWaitingTime { get => MaxWaitingTime/ StepsCount;}
        public int PassedParticipantsAmount { get; private set; }
        Crossroad crossroad;
        public TrafficStatistics(Crossroad crossroad)
        {
            this.crossroad = crossroad;
            crossroad.UpdateNotify += UpdateStatistics;
        }
        public override string ToString()
        {
            foreach (var l in crossroad.TrafficLights)
            {
                PassedParticipantsAmount += l.PassedCount;
            }
            return $"Максимальное время оижадния: {MaxWaitingTime}, Среднее время ожидания: {AwarageMaxWaitingTime}, Количество проехавших: {PassedParticipantsAmount}";
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
