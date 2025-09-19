using System;

class RemoteControlCar
{
    // TODO: define the constructor for the 'RemoteControlCar' class
    private int _speed;
    private int _batteryDrain;
    private int _distanceDriven;
    private int _battery;

    public RemoteControlCar(int speed , int batteryDrain)
    {
        _speed = speed;
        _batteryDrain = batteryDrain;
        _distanceDriven = 0;
        _battery = 100; // start with full battery
    }
    // check if battery is drained
    public bool BatteryDrained()
    {
        return _battery < _batteryDrain;
    }
    // return distance driven
    public int DistanceDriven()
    {
        return _distanceDriven;
    }
    //
    public void Drive()
    {
        if (!BatteryDrained())
        {
            _distanceDriven += _speed;
            _battery -= _batteryDrain;
        }
    }

    public static RemoteControlCar Nitro()
    {
        return new RemoteControlCar(50, 4);
    }
}

class RaceTrack
{
    // TODO: define the constructor for the 'RaceTrack' class
    private int _distance;

    public RaceTrack(int distance)
    {
        _distance = distance;
    }

    public bool TryFinishTrack(RemoteControlCar car)
    {
        int maxDistance = (100 / carBatteryDrain(car)) * carSpeed(car);
        return maxDistance >= _distance;
    }

    private int carSpeed(RemoteControlCar car)
    {
        var type = typeof(RemoteControlCar);
        var field = type.GetField("_speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (int)field.GetValue(car);
    }
    private int carBatteryDrain(RemoteControlCar car)
    {
        var type = typeof(RemoteControlCar);
        var field = type.GetField("_batteryDrain", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (int)field.GetValue(car);
    }
}
