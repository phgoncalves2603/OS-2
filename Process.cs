namespace CpuSchedulingWinForms
{
    public class Process
    {
        public int Id { get; set; }
        public double ArrivalTime { get; set; }
        public double BurstTime { get; set; }
        public double RemainingTime { get; set; }
        public double Priority { get; set; }
        public double WaitingTime { get; set; }
        public double TurnaroundTime { get; set; }
        public double ResponseTime { get; set; }
        public double CompletionTime { get; set; }
        public bool HasStarted { get; set; }
        public int CurrentQueue { get; set; }

        public Process(int id, double arrivalTime, double burstTime, double priority = 0)
        {
            Id = id;
            ArrivalTime = arrivalTime;
            BurstTime = burstTime;
            RemainingTime = burstTime;
            Priority = priority;
            HasStarted = false;
            CurrentQueue = 0;
        }
    }
}