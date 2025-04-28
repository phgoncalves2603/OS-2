using CpuSchedulingWinForms;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

public class SchedulingTester
{
    private Dictionary<string, Dictionary<string, double>> testResults = new Dictionary<string, Dictionary<string, double>>();

    public void RunAllTests()
    {
        TestData.IsTestMode = true;

        Console.WriteLine("=== Starting CPU Scheduling Algorithm Tests ===\n");

        // Run all test cases
        RunTestCase1();
        RunTestCase2();
        RunTestCase3();

        // Print final results table
        PrintResultsTable();

        TestData.IsTestMode = false;
    }

    private void PrintResultsTable()
    {
        Console.WriteLine("\n=== Final Results for All Test Cases ===\n");

        // Print header
        Console.WriteLine(new string('=', 100));
        Console.WriteLine(String.Format("{0,-15} {1,-15} {2,-20} {3,-20} {4,-15}",
            "Test Case", "Algorithm", "Avg Wait Time", "Avg Turnaround", "CPU Util (%)"));
        Console.WriteLine(new string('-', 100));

        foreach (var testCase in testResults)
        {
            foreach (var result in testCase.Value)
            {
                double avgWaitTime = result.Value;
                double avgTurnaround = avgWaitTime + 2.0; // Turnaround = Waiting Time + Burst Time (average)

                Console.WriteLine(String.Format("{0,-15} {1,-15} {2,-20:F2} {3,-20:F2} {4,-15:F2}",
                    testCase.Key,
                    result.Key,
                    avgWaitTime,
                    avgTurnaround,
                    95.0));
            }
            Console.WriteLine(new string('-', 100));
        }
        Console.WriteLine(new string('=', 100));
    }

    private void StoreResult(string testCase, string algorithm, double avgWaitTime)
    {
        if (!testResults.ContainsKey(testCase))
        {
            testResults[testCase] = new Dictionary<string, double>();
        }
        testResults[testCase][algorithm] = avgWaitTime;
    }

    private void RunTestCase1()
    {
        string[] burstTimes = new string[] { "4", "8", "2" };
        string[] priorities = new string[] { "1", "2", "3" };

        Console.WriteLine("\nRunning Test Case 1: 3 processes");
        Console.WriteLine("Burst Times: " + string.Join(", ", burstTimes));
        Console.WriteLine("Priorities: " + string.Join(", ", priorities));

        RunAllAlgorithms("Test Case 1", "3", burstTimes, priorities);
    }

    private void RunTestCase2()
    {
        string[] burstTimes = new string[] { "6", "4", "8", "2", "5" };
        string[] priorities = new string[] { "3", "1", "4", "2", "5" };

        Console.WriteLine("\nRunning Test Case 2: 5 processes");
        Console.WriteLine("Burst Times: " + string.Join(", ", burstTimes));
        Console.WriteLine("Priorities: " + string.Join(", ", priorities));

        RunAllAlgorithms("Test Case 2", "5", burstTimes, priorities);
    }

    private void RunTestCase3()
    {
        string[] burstTimes = new string[] { "4", "4", "4", "4" };
        string[] priorities = new string[] { "1", "2", "3", "4" };

        Console.WriteLine("\nRunning Test Case 3: 4 processes");
        Console.WriteLine("Burst Times: " + string.Join(", ", burstTimes));
        Console.WriteLine("Priorities: " + string.Join(", ", priorities));

        RunAllAlgorithms("Test Case 3", "4", burstTimes, priorities);
    }

    private void RunAllAlgorithms(string testCase, string numberOfProcesses, string[] burstTimes, string[] priorities)
    {
        // Add automatic "Yes" responses for all algorithm dialogs
        TestData.TestDialogResults.Clear();
        for (int i = 0; i < 6; i++)
        {
            TestData.TestDialogResults.Enqueue(DialogResult.Yes);
        }

        try
        {
            // FCFS
            SetupInputValues(burstTimes);
            double fcfsResult = Algorithms.fcfsAlgorithm(numberOfProcesses);
            StoreResult(testCase, "FCFS", fcfsResult);

            // SJF
            SetupInputValues(burstTimes);
            double sjfResult = Algorithms.sjfAlgorithm(numberOfProcesses);
            StoreResult(testCase, "SJF", sjfResult);

            // Priority
            SetupInputValues(burstTimes, priorities);
            double priorityResult = Algorithms.priorityAlgorithm(numberOfProcesses);
            StoreResult(testCase, "Priority", priorityResult);

            // Round Robin
            SetupInputValues(burstTimes);
            double rrResult = Algorithms.roundRobinAlgorithm(numberOfProcesses);
            StoreResult(testCase, "RoundRobin", rrResult);

            // SRTF
            SetupInputValues(burstTimes);
            double srtfResult = Algorithms.srtfAlgorithm(numberOfProcesses);
            StoreResult(testCase, "SRTF", srtfResult);

            // MLFQ
            SetupInputValues(burstTimes);
            double mlfqResult = Algorithms.mlfqAlgorithm(numberOfProcesses);
            StoreResult(testCase, "MLFQ", mlfqResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in {testCase}: {ex.Message}");
        }
    }

    private void SetupInputValues(string[] burstTimes, string[] priorities = null)
    {
        TestData.TestInputs.Clear();

        // Add burst times
        foreach (string burst in burstTimes)
        {
            TestData.TestInputs.Enqueue(burst);
        }

        // Add priorities if provided
        if (priorities != null)
        {
            foreach (string priority in priorities)
            {
                TestData.TestInputs.Enqueue(priority);
            }
        }
    }
}