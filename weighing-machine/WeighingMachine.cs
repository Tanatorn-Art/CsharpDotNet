using System.Diagnostics.Contracts;

class WeighingMachine
{
    private double _weight;

    public WeighingMachine(int precision)
    { 
        Precision = precision;
    }

    public int Precision { get; }

    public double Weight
    {
        get { return _weight; }
        set
        {
            if (value < 0)
            { 
                throw new ArgumentOutOfRangeException(nameof(value), "Weight cannot be negative");
            }
            _weight = value;
        }
    }

    public double TareAdjustment { get; set; } = 5.0;

    public string DisplayWeight
    {
        get
        { 
            double adjustedWeight = Weight - TareAdjustment;
            return $"{adjustedWeight.ToString($"F{Precision}")} kg";
        }
    }
}
