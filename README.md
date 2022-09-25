This sample demonstrates thread starvation issues that may occur when asynchronous operations are blocked.

Running the sample:

1. Install dotnet performance counters tool from https://docs.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-counters

```
dotnet tool install --global dotnet-counters
```

2. Go to Solution/Properties and use Multiple startup projects. Make sure both TS and TSClient have Action set to Start.
3. Run the solution.
4. Open a command prompt and run dotnet-counters

```
dotnet-counters monitor --name iisexpress
```

5. Run the TSClient by pressing "S"

6. Observe ThreadPool Thread Count

To observe various behaviours:

1. No issues with quick synchronous operation

Make sure TS project Properties/Application/General/Target framework is set to 5.0 (and framework is installed on your machine).
Make sure in TS project `WeatherForecastController` only lines under `// quick synchronous operation` are uncommented.
TSClient will report equal number of "s" sent requests and "r" received responses in the console.
ThreadPool Thread Count will stabilize and not rise.

2.  No issues with long asynchronous operation

Make sure TS project Properties/Application/General/Target framework is set to 5.0 (and framework is installed on your machine).
Make sure in TS project `WeatherForecastController` only lines under `// long aynchronous operation` are uncommented.
TSClient will report number of "s" sent requests then stabilize and report equal number of "s" sent requests and "r" received responses in the console.
ThreadPool Thread Count will rise then stabilize and not rise again.

3.  Issues with blocking long asynchronous operation

Make sure TS project Properties/Application/General/Target framework is set to 5.0 (and framework is installed on your machine).
Make sure in TS project `WeatherForecastController` only lines under `// block long asynchronous operation` are uncommented.
TSClient will report more "s" sent requests then "r" received responses in the console. Eventually no responses will be received.
ThreadPool Thread Count will rise and never stabilize. Thread Queue Length will rise as well.

4.  Resolved issues with blocking long asynchronous operation using .net 6.0

Make sure TS project Properties/Application/General/Target framework is set to 6.0 (and framework is installed on your machine).
Make sure in TS project `WeatherForecastController` only lines under `// block long asynchronous operation` are uncommented.
TSClient will report more "s" sent requests then "r" received responses in the console.
ThreadPool Thread Count will rise higher then in scenario 2, then stabilize and not rise again.

5.  Issues with blocking long asynchronous operation in a nonstandard way using .net 6.0

Make sure TS project Properties/Application/General/Target framework is set to 6.0 (and framework is installed on your machine).
Make sure in TS project `WeatherForecastController` only lines under `// block long asynchronous operation in a non-standard way` are uncommented.
TSClient will report more "s" sent requests then "r" received responses in the console. Eventually no responses will be received.
ThreadPool Thread Count will rise and never stabilize. Thread Queue Length will rise as well.
