using System;

public class CalculationException : Exception
{
    public CalculationException(int operand1, int operand2, string message, Exception inner)
        : base(message, inner)
    {
        this.Operand1 = operand1;
        this.Operand2 = operand2;
    }

    public int Operand1 { get; }
    public int Operand2 { get; }
}

public class CalculatorTestHarness
{
    private Calculator calculator;

    public CalculatorTestHarness(Calculator calculator)
    {
        this.calculator = calculator;
    }

    public string TestMultiplication(int x, int y)
    {
        try
        {
            Multiply(x, y);
            return "Multiply succeeded";
        }
        catch (CalculationException ex) when (ex.Operand1 < 0 && ex.Operand2 < 0)
        {
            return $"Multiply failed for negative operands. {ex.InnerException.Message}";
        }
        catch (CalculationException ex)
        {
            return $"Multiply failed for mixed or positive operands. {ex.InnerException.Message}";
        }
    }

    public void Multiply(int x, int y)
    {
        try
        {
            calculator.Multiply(x, y);
        }
        catch (OverflowException ex)
        {
            throw new CalculationException(x, y, "An overflow occurred during multiplication.", ex);
        }
    }
}

// Please do not modify the code below.
public class Calculator
{
    public int Multiply(int x, int y)
    {
        checked
        {
            return x * y;
        }
    }
}