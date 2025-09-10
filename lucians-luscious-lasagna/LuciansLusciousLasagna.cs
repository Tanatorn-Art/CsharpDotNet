
class Lasagna
{
    // TODO: define the 'ExpectedMinutesInOven()' method
    public int ExpectedMinutesInOven() {
        return 40;
        Console.WriteLine($"Expected minutes in oven: {ExpectedMinutesInOven()}");
    }
    // TODO: define the 'RemainingMinutesInOven()' method
    public int RemainingMinutesInOven(int actualMinutes) {
        return ExpectedMinutesInOven() - actualMinutes;
        Console.WriteLine($"Remaining minutes in oven: {RemainingMinutesInOven(actualMinutes)}");
    }   
    // TODO: define the 'PreparationTimeInMinutes()' method
    public int PreparationTimeInMinutes(int layer) {
        return layer * 2;
        Console.WriteLine($"Preparation time in minutes: {PreparationTimeInMinutes(layer)}");
    }    
    // TODO: define the 'ElapsedTimeInMinutes()' method
    public int ElapsedTimeInMinutes(int layer, int actualMinutes) {
        return PreparationTimeInMinutes(layer) + actualMinutes;
        Console.WriteLine($"Elapsed time in minutes: {ElapsedTimeInMinutes(layer, actualMinutes)}");
    }
}