public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string? operation)
    {
        if (operation == null)
        { 
            throw new ArgumentNullException(nameof(operation));
        }

        if (operation == "")
        { 
            throw new ArgumentException("Operation cannot be empty", nameof(operation));
        }

        switch (operation)
        {
            case "+":
                int addResult = operand1 + operand2;
                return $"{operand1} + {operand2} = {addResult}";
            case "*":
                int mutiplyResult = operand1 * operand2;
                return $"{operand1} * {operand2} = {mutiplyResult}";
            case "/":
                if (operand2 == 0)
                {
                    return "Division by zero is not allowed.";
                }
                int divideResult = operand1 / operand2;
                return $"{operand1} / {operand2} = {divideResult}";

            default:
                throw new ArgumentOutOfRangeException(nameof(operation), $"Operation '{operation}' is not supported.");
        }
    }
}
