using System;
using System.Collections.Generic;
using System.Linq;

public class Reactor
{
    public InputCell CreateInputCell(int value)
        => new InputCell { Value = value };

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
        => new ComputeCell(producers.ToArray(), compute);
}

public abstract class Cell
{
    int _value;

    public int Value
    {
        get => _value;
        set
        {
            int prev = _value;

            _value = value;

            if (prev != _value)
                Changed?.Invoke(this, value);
        }
    }

    public event EventHandler<int> Changed;
}

public class InputCell : Cell { }

public class ComputeCell : Cell
{
    Cell[] Cells { get; }
    Func<int[], int> Formula { get; }

    public ComputeCell(Cell[] cells, Func<int[], int> formula)
    {
        Cells = cells;
        Formula = formula;

        Subscribe();

        Compute();
    }

    void OnChanged(object? sender, int e)
    {
        foreach (ComputeCell computeCell in Cells.OfType<ComputeCell>())
            computeCell.Compute();

        Compute();
    }

    void Subscribe()
    {
        foreach (Cell cell in Cells)
            cell.Changed += OnChanged;
    }

    void Unsubscribe()
    {
        foreach (Cell cell in Cells)
            cell.Changed -= OnChanged;
    }

    void Compute()
    {
        Value = Formula(Cells.Select(cell => cell.Value).ToArray());
    }

    ~ComputeCell()
    {
        Unsubscribe();
    }
}