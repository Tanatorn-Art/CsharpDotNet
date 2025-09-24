using System;

public struct CurrencyAmount
{
    private decimal amount;
    private string currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        this.amount = amount;
        this.currency = currency;
    }

    private static void EnsureSameCurrency(CurrencyAmount a, CurrencyAmount b)
    {
        if (a.currency != b.currency)
            throw new ArgumentException("Currency mismatch");
    }

    // ✅ Equality operators
    public static bool operator ==(CurrencyAmount a, CurrencyAmount b)
    {
        EnsureSameCurrency(a, b);
        return a.amount == b.amount;
    }

    public static bool operator !=(CurrencyAmount a, CurrencyAmount b)
    {
        EnsureSameCurrency(a, b);
        return a.amount != b.amount;
    }

    // ✅ Comparison operators
    public static bool operator >(CurrencyAmount a, CurrencyAmount b)
    {
        EnsureSameCurrency(a, b);
        return a.amount > b.amount;
    }

    public static bool operator <(CurrencyAmount a, CurrencyAmount b)
    {
        EnsureSameCurrency(a, b);
        return a.amount < b.amount;
    }

    // ✅ Arithmetic operators
    public static CurrencyAmount operator +(CurrencyAmount a, CurrencyAmount b)
    {
        EnsureSameCurrency(a, b);
        return new CurrencyAmount(a.amount + b.amount, a.currency);
    }

    public static CurrencyAmount operator -(CurrencyAmount a, CurrencyAmount b)
    {
        EnsureSameCurrency(a, b);
        return new CurrencyAmount(a.amount - b.amount, a.currency);
    }

    public static CurrencyAmount operator *(CurrencyAmount a, decimal multiplier)
    {
        return new CurrencyAmount(a.amount * multiplier, a.currency);
    }

    public static CurrencyAmount operator /(CurrencyAmount a, decimal divisor)
    {
        return new CurrencyAmount(a.amount / divisor, a.currency);
    }

    // ✅ Type conversions
    public static explicit operator double(CurrencyAmount a) => (double)a.amount;

    public static implicit operator decimal(CurrencyAmount a) => a.amount;

    // override Equals & GetHashCode for completeness
    public override bool Equals(object obj)
    {
        if (obj is CurrencyAmount other)
        {
            if (currency != other.currency) return false;
            return amount == other.amount;
        }
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(amount, currency);

    public override string ToString() => $"{{{amount}, {currency}}}";
}
