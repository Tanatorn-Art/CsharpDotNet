using System;
using System.Collections.Generic;
using System.Linq;

public class CustomSet : IEquatable<CustomSet>
{
    private readonly HashSet<int> _elements;

    public CustomSet(params int[] values)
    {
        _elements = new HashSet<int>(values);
    }

    public CustomSet Add(int value)
    {
        var newSet = new CustomSet(_elements.ToArray());
        newSet._elements.Add(value);
        return newSet;
    }

    public bool Empty()
    {
        return _elements.Count == 0;
    }

    public bool Contains(int value)
    {
        return _elements.Contains(value);
    }

    public bool Subset(CustomSet right)
    {
        if (right == null)
            return false;

        return _elements.IsSubsetOf(right._elements);
    }

    public bool Disjoint(CustomSet right)
    {
        if (right == null)
            return true;

        return !_elements.Overlaps(right._elements);
    }

    public CustomSet Intersection(CustomSet right)
    {
        if (right == null)
            return new CustomSet();

        var intersection = _elements.Intersect(right._elements).ToArray();
        return new CustomSet(intersection);
    }

    public CustomSet Difference(CustomSet right)
    {
        if (right == null)
            return new CustomSet(_elements.ToArray());

        var difference = _elements.Except(right._elements).ToArray();
        return new CustomSet(difference);
    }

    public CustomSet Union(CustomSet right)
    {
        if (right == null)
            return new CustomSet(_elements.ToArray());

        var union = _elements.Union(right._elements).ToArray();
        return new CustomSet(union);
    }

    // Custom equality comparison
    public bool Equals(CustomSet other)
    {
        if (other == null)
            return false;

        return _elements.SetEquals(other._elements);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as CustomSet);
    }

    public override int GetHashCode()
    {
        // Order-independent hash code for sets
        int hash = 0;
        foreach (int element in _elements)
        {
            hash ^= element.GetHashCode();
        }
        return hash;
    }

    public static bool operator ==(CustomSet left, CustomSet right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(CustomSet left, CustomSet right)
    {
        return !(left == right);
    }
}