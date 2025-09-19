using System;
using System.Collections.Generic;
using System.Linq;

public enum StopwatchState
{
    Ready,
    Running,
    Stopped
}

public class SplitSecondStopwatch
{
    private readonly TimeProvider _timeProvider;
    private DateTimeOffset _lastStartTime;
    private TimeSpan _currentLapTime;
    private List<TimeSpan> _laps;

    public StopwatchState State { get; private set; }

    public TimeSpan CurrentLap => State == StopwatchState.Running
        ? _currentLapTime + (_timeProvider.GetUtcNow() - _lastStartTime)
        : _currentLapTime;

    public TimeSpan Total => _laps.Aggregate(CurrentLap, (sum, lap) => sum + lap);

    public IReadOnlyCollection<TimeSpan> PreviousLaps => _laps.AsReadOnly();

    public SplitSecondStopwatch(TimeProvider time)
    {
        _timeProvider = time;
        _laps = new List<TimeSpan>();
        State = StopwatchState.Ready;
        _currentLapTime = TimeSpan.Zero;
    }

    public void Start()
    {
        if (State == StopwatchState.Running)
            throw new InvalidOperationException("Stopwatch is already running.");

        _lastStartTime = _timeProvider.GetUtcNow();
        State = StopwatchState.Running;
    }

    public void Stop()
    {
        if (State != StopwatchState.Running)
            throw new InvalidOperationException("Stop cannot be called unless stopwatch is running.");

        _currentLapTime += _timeProvider.GetUtcNow() - _lastStartTime;
        State = StopwatchState.Stopped;
    }

    public void Lap()
    {
        if (State != StopwatchState.Running)
            throw new InvalidOperationException("Lap cannot be called unless stopwatch is running.");

        _currentLapTime += _timeProvider.GetUtcNow() - _lastStartTime;
        _laps.Add(_currentLapTime);
        _currentLapTime = TimeSpan.Zero;
        _lastStartTime = _timeProvider.GetUtcNow();
    }

    public void Reset()
    {
        if (State != StopwatchState.Stopped)
            throw new InvalidOperationException("Reset can only be called from stopped state.");

        _laps.Clear();
        _currentLapTime = TimeSpan.Zero;
        State = StopwatchState.Ready;
    }

}
