# CPU Scheduling Simulator

A Windows Forms application that simulates various CPU scheduling algorithms with a graphical user interface. This simulator helps understand and compare different CPU scheduling algorithms commonly used in operating systems.

## Features

- Implementation of 6 CPU scheduling algorithms:
  - First Come First Serve (FCFS)
  - Shortest Job First (SJF)
  - Priority Scheduling
  - Round Robin
  - Shortest Remaining Time First (SRTF)
  - Multi-Level Feedback Queue (MLFQ)
- Visual representation of process execution
- Performance metrics calculation:
  - Average Waiting Time
  - Average Turnaround Time
  - CPU Utilization
- Automated testing functionality
- Process visualization
- Interactive GUI interface

## Prerequisites

- Windows Operating System
- .NET Framework 4.7.2 or higher
- Visual Studio 2019 or higher (for development)

## Building the Project

1. Clone the repository:
```bash
git clone [https://github.com/phgoncalves2603/OS-2.git]
```

2. Open the solution:
- Navigate to the project directory
- Open `CpuSchedulingWinForms.sln` in Visual Studio

3. Build the solution:
- In Visual Studio, select `Build > Build Solution`
- Or press `Ctrl + Shift + B`

## Running the Application

### Method 1: Through Visual Studio
1. Open the solution in Visual Studio
2. Press `F5` or click the "Start" button
3. The application will launch in debug mode

### Method 2: Direct Execution
1. Navigate to `bin/Debug` or `bin/Release` folder
2. Run `CpuSchedulingWinForms.exe`

## Using the Simulator

1. **Main Interface**
   - The application opens to a dashboard with multiple tabs
   - Navigate using the sidebar buttons

2. **Running Scheduling Algorithms**
   - Go to the "CPU Scheduler" tab
   - Enter the number of processes
   - Select an algorithm to run
   - Input burst times and priorities (if required)
   - View the results

3. **Running Tests**
   - Click the "Run Tests" button
   - The application will run through predefined test cases
   - Results will be displayed in a table format

## Test Cases

### Test Case 1: Basic Test (3 Processes)
| Process | Burst Time | Priority |
|:-------:|:----------:|:--------:|
| P1      | 4          | 1        |
| P2      | 8          | 2        |
| P3      | 2          | 3        |

**Description:**  
- Small test case to verify basic functionality.
- Mix of burst times to test short and long processes.
- Sequential priorities (1, 2, 3).

---

### Test Case 2: Medium Load (5 Processes)
| Process | Burst Time | Priority |
|:-------:|:----------:|:--------:|
| P1      | 6          | 3        |
| P2      | 4          | 1        |
| P3      | 8          | 4        |
| P4      | 2          | 2        |
| P5      | 5          | 5        |

**Description:**  
- Medium-sized test with more varied workloads.
- Mix of short and long processes.
- Non-sequential priorities (tests priority handling).

---

### Test Case 3: Edge Case (4 Processes)
| Process | Burst Time | Priority |
|:-------:|:----------:|:--------:|
| P1      | 4          | 1        |
| P2      | 4          | 2        |
| P3      | 4          | 3        |
| P4      | 4          | 4        |

**Description:**  
- Edge case test with identical burst times.
- Sequential priorities.
- Good for testing fairness in scheduling.


## Algorithm Implementations

1. **FCFS (First Come First Serve)**
   - Non-preemptive
   - Processes are executed in arrival order
   - Simple and fair for equal burst times
   - May lead to convoy effect with varying burst times

2. **SJF (Shortest Job First)**
   - Non-preemptive
   - Selects process with shortest burst time
   - Optimal for average waiting time
   - May lead to starvation of longer processes

3. **Priority Scheduling**
   - Processes are executed based on priority
   - Lower number indicates higher priority
   - Supports dynamic priority changes
   - May lead to priority inversion

4. **Round Robin**
   - Time quantum based scheduling
   - Default quantum = 2 time units
   - Fair distribution of CPU time
   - Good for time-sharing systems

5. **SRTF (Shortest Remaining Time First)**
   - Preemptive version of SJF
   - Switches to shorter processes when they arrive
   - Optimal for average waiting time
   - Higher overhead due to frequent context switches

6. **MLFQ (Multi-Level Feedback Queue)**
   - Multiple priority queues
   - Dynamic priority adjustment
   - Time quantum varies by queue level
   - Balances throughput and response time

## Performance Metrics

The simulator calculates and displays the following metrics:

1. **Average Waiting Time (AWT)**
   - Time processes spend waiting in ready queue
   - Lower is better
   - Formula: (Total Waiting Time) / (Number of Processes)

2. **Average Turnaround Time (ATT)**
   - Total time from submission to completion
   - Includes waiting time and execution time
   - Formula: (Total Turnaround Time) / (Number of Processes)

3. **CPU Utilization**
   - Percentage of time CPU is busy
   - Higher is better
   - Formula: (Total Burst Time) / (Total Time) × 100

## Known Limitations

1. All processes are assumed to be CPU-bound
2. No I/O operations are simulated
3. Process arrival times are simplified
4. Memory management is not implemented

## Troubleshooting

1. **Build Errors**
   - Ensure .NET Framework is properly installed
   - Clean and rebuild solution
   - Check for missing references

2. **Runtime Errors**
   - Verify input values are valid numbers
   - Check process count is within reasonable limits
   - Ensure sufficient system resources

3. **Display Issues**
   - Verify minimum screen resolution (1024x768)
   - Check Windows DPI settings
   - Update graphics drivers if necessary

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Version History

- 1.0.0
  - Initial Release
  - Basic algorithm implementations
  - Simple GUI interface

- 1.1.0
  - Added automated testing
  - Improved performance metrics
  - Bug fixes and optimizations

## License

This project is licensed under the MIT License - see the LICENSE file for details

## Acknowledgments

- Based on operating systems scheduling concepts
- GUI implementation using Windows Forms
- Testing framework inspiration from various CPU scheduling simulators