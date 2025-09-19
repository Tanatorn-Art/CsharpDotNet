public class RemoteControlCar
{
    private int _distanceDriven;
    private int _batteryPercentage;

    public RemoteControlCar()
    {
        _distanceDriven = 0;
        _batteryPercentage = 100;
    }

    // Task 1: Buy a new car
    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    // Task 2: Display distance
    public string DistanceDisplay()
    {
        return $"Driven {_distanceDriven} meters";
    }

    // Task 3 & 6: Display battery
    public string BatteryDisplay()
    {
        return _batteryPercentage > 0
            ? $"Battery at {_batteryPercentage}%"
            : "Battery empty";
    }

    // Task 4,5,6: Drive the car
    public void Drive()
    {
        if (_batteryPercentage <= 0)
            return;

        _distanceDriven += 20;
        _batteryPercentage -= 1;
    }
}
