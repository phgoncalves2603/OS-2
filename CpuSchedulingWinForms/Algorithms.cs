using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static double fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double twt = 0.0, awt = 0.0;

            DialogResult result = TestData.ShowDialog("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int num = 0; num < np; num++)
                {
                    string input = TestData.GetInput("Enter Burst time: ", "Burst time for P" + (num + 1));
                    bp[num] = Convert.ToInt64(input);
                }

                for (int num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        TestData.ShowDialog("Waiting time for P" + (num + 1) + " = " + wtp[num],
                            "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }

                for (int num = 0; num < np; num++)
                {
                    twt += wtp[num];
                }
                awt = twt / np;
                TestData.ShowDialog("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)",
                    "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);
            }

            return awt;
        }

        public static double sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt = 0.0;
            int x;
            double temp;
            bool found = false;

            DialogResult result = TestData.ShowDialog("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int num = 0; num < np; num++)
                {
                    string input = TestData.GetInput("Enter burst time: ", "Burst time for P" + (num + 1));
                    bp[num] = Convert.ToInt64(input);
                    p[num] = bp[num];
                }

                // Sort processes by burst time
                for (x = 0; x < np - 1; x++)
                {
                    for (int num = 0; num < np - 1; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }

                for (int num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                        TestData.ShowDialog("Waiting time for P1 = 0", "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + p[num - 1];
                        TestData.ShowDialog("Waiting time for P" + (num + 1) + " = " + wtp[num],
                            "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }

                for (int num = 0; num < np; num++)
                {
                    twt += wtp[num];
                }
                awt = twt / np;
                TestData.ShowDialog("Average waiting time = " + awt + " sec(s)",
                    "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return awt;
        }

        public static double priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double awt = 0.0;

            DialogResult result = TestData.ShowDialog("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTime = new double[np];
                int[] priority = new int[np];
                double[] waitingTime = new double[np];
                int[] processOrder = new int[np];

                // Input burst times and priorities
                for (int i = 0; i < np; i++)
                {
                    string input = TestData.GetInput("Enter burst time: ", "Burst time for P" + (i + 1));
                    burstTime[i] = Convert.ToInt64(input);

                    string priorityInput = TestData.GetInput("Enter priority: ", "Priority for P" + (i + 1));
                    priority[i] = Convert.ToInt16(priorityInput);
                    processOrder[i] = i;
                }

                // Sort by priority
                for (int i = 0; i < np - 1; i++)
                {
                    for (int j = 0; j < np - 1 - i; j++)
                    {
                        if (priority[j] > priority[j + 1])
                        {
                            // Swap all properties
                            Swap(ref priority[j], ref priority[j + 1]);
                            Swap(ref burstTime[j], ref burstTime[j + 1]);
                            Swap(ref processOrder[j], ref processOrder[j + 1]);
                        }
                    }
                }

                // Calculate waiting times
                waitingTime[0] = 0;
                double totalWait = 0;

                for (int i = 1; i < np; i++)
                {
                    waitingTime[i] = waitingTime[i - 1] + burstTime[i - 1];
                    totalWait += waitingTime[i];
                    TestData.ShowDialog("Waiting time for P" + (processOrder[i] + 1) + " = " + waitingTime[i],
                        "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

                awt = totalWait / np;
                TestData.ShowDialog("Average waiting time = " + awt.ToString("F2") + " sec(s)",
                    "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return awt;
        }

        public static double roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double awt = 0.0;

            DialogResult result = TestData.ShowDialog("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTime = new double[np];
                double[] waitingTime = new double[np];
                double[] remainingTime = new double[np];
                double quantum = 2; // Fixed quantum time
                double currentTime = 0;

                // Input burst times
                for (int i = 0; i < np; i++)
                {
                    string input = TestData.GetInput("Enter burst time: ", "Burst time for P" + (i + 1));
                    burstTime[i] = Convert.ToInt64(input);
                    remainingTime[i] = burstTime[i];
                }

                bool done = false;
                while (!done)
                {
                    done = true;
                    for (int i = 0; i < np; i++)
                    {
                        if (remainingTime[i] > 0)
                        {
                            done = false;
                            if (remainingTime[i] > quantum)
                            {
                                currentTime += quantum;
                                remainingTime[i] -= quantum;
                            }
                            else
                            {
                                currentTime += remainingTime[i];
                                waitingTime[i] = currentTime - burstTime[i];
                                remainingTime[i] = 0;
                            }
                        }
                    }
                }

                double totalWait = 0;
                for (int i = 0; i < np; i++)
                {
                    TestData.ShowDialog("Waiting time for P" + (i + 1) + " = " + waitingTime[i],
                        "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                    totalWait += waitingTime[i];
                }

                awt = totalWait / np;
                TestData.ShowDialog("Average waiting time = " + awt.ToString("F2") + " sec(s)",
                    "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return awt;
        }

        public static double srtfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double awt = 0.0;

            DialogResult result = TestData.ShowDialog("Shortest Remaining Time First", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTime = new double[np];
                double[] waitingTime = new double[np];
                double[] remainingTime = new double[np];
                double[] completionTime = new double[np];
                double currentTime = 0;

                // Input burst times
                for (int i = 0; i < np; i++)
                {
                    string input = TestData.GetInput("Enter burst time: ", "Burst time for P" + (i + 1));
                    burstTime[i] = Convert.ToInt64(input);
                    remainingTime[i] = burstTime[i];
                }

                int complete = 0;
                while (complete < np)
                {
                    int shortest = -1;
                    double minBurst = double.MaxValue;

                    for (int i = 0; i < np; i++)
                    {
                        if (remainingTime[i] > 0 && remainingTime[i] < minBurst)
                        {
                            minBurst = remainingTime[i];
                            shortest = i;
                        }
                    }

                    if (shortest == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    remainingTime[shortest]--;
                    currentTime++;

                    if (remainingTime[shortest] == 0)
                    {
                        complete++;
                        completionTime[shortest] = currentTime;
                        waitingTime[shortest] = completionTime[shortest] - burstTime[shortest];
                        TestData.ShowDialog("Waiting time for P" + (shortest + 1) + " = " + waitingTime[shortest],
                            "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }

                double totalWait = 0;
                for (int i = 0; i < np; i++)
                {
                    totalWait += waitingTime[i];
                }

                awt = totalWait / np;
                TestData.ShowDialog("Average waiting time = " + awt.ToString("F2") + " sec(s)",
                    "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return awt;
        }

        public static double mlfqAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double awt = 0.0;

            DialogResult result = TestData.ShowDialog("Multi-Level Feedback Queue", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTime = new double[np];
                double[] waitingTime = new double[np];
                double[] remainingTime = new double[np];
                int[] queueLevel = new int[np];
                double[] completionTime = new double[np];
                double currentTime = 0;

                // Input burst times
                for (int i = 0; i < np; i++)
                {
                    string input = TestData.GetInput("Enter burst time: ", "Burst time for P" + (i + 1));
                    burstTime[i] = Convert.ToInt64(input);
                    remainingTime[i] = burstTime[i];
                    queueLevel[i] = 0; // All processes start in highest priority queue
                }

                int complete = 0;
                int maxLevel = 3; // Number of queue levels
                double[] timeQuantum = new double[] { 2, 4, 8 }; // Time quantum for each level

                while (complete < np)
                {
                    int current = -1;

                    // Find highest priority process
                    for (int level = 0; level < maxLevel; level++)
                    {
                        for (int i = 0; i < np; i++)
                        {
                            if (remainingTime[i] > 0 && queueLevel[i] == level)
                            {
                                current = i;
                                break;
                            }
                        }
                        if (current != -1) break;
                    }

                    if (current == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    double quantum = timeQuantum[queueLevel[current]];
                    if (remainingTime[current] > quantum)
                    {
                        currentTime += quantum;
                        remainingTime[current] -= quantum;
                        queueLevel[current] = Math.Min(queueLevel[current] + 1, maxLevel - 1);
                    }
                    else
                    {
                        currentTime += remainingTime[current];
                        waitingTime[current] = currentTime - burstTime[current];
                        remainingTime[current] = 0;
                        complete++;
                        TestData.ShowDialog("Waiting time for P" + (current + 1) + " = " + waitingTime[current],
                            "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }

                double totalWait = 0;
                for (int i = 0; i < np; i++)
                {
                    totalWait += waitingTime[i];
                }

                awt = totalWait / np;
                TestData.ShowDialog("Average waiting time = " + awt.ToString("F2") + " sec(s)",
                    "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return awt;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}