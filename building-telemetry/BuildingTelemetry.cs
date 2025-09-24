using System;

public class RemoteControlCar
{
    private int batteryPercentage = 100;
    private int distanceDrivenInMeters = 0;
    private string[] sponsors = new string[0];
    private int latestSerialNum = 0;

    public void Drive()
    {
        if (batteryPercentage > 0)
        {
            batteryPercentage -= 10;
            distanceDrivenInMeters += 2;
        }
    }

    // ✅ 1. Store sponsors
    public void SetSponsors(params string[] sponsors)
    {
        this.sponsors = sponsors;
    }

    public string DisplaySponsor(int sponsorNum)
    {
        return sponsors[sponsorNum];
    }

    // ✅ 2. Telemetry data
    public bool GetTelemetryData(ref int serialNum,
        out int batteryPercentage, out int distanceDrivenInMeters)
    {
        if (serialNum < latestSerialNum)
        {
            serialNum = latestSerialNum;
            batteryPercentage = -1;
            distanceDrivenInMeters = -1;
            return false;
        }

        latestSerialNum = serialNum;
        batteryPercentage = this.batteryPercentage;
        distanceDrivenInMeters = this.distanceDrivenInMeters;
        return true;
    }

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }
}

public class TelemetryClient
{
    private RemoteControlCar car;

    public TelemetryClient(RemoteControlCar car)
    {
        this.car = car;
    }

    // ✅ 3. Battery usage per meter
    public string GetBatteryUsagePerMeter(int serialNum)
    {
        int currentSerial = serialNum;
        bool ok = car.GetTelemetryData(ref currentSerial,
            out int batteryPercentage, out int distanceDrivenInMeters);

        if (!ok || distanceDrivenInMeters == 0)
        {
            return "no data";
        }

        int usagePerMeter = (100 - batteryPercentage) / distanceDrivenInMeters;
        return $"usage-per-meter={usagePerMeter}";
    }
}
