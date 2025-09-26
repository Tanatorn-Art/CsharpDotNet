using System;
using System.Collections.Generic;

public class BinTree
{
    public BinTree(int value, BinTree? left, BinTree? right)
    {
        Value = value;
        Left = left;
        Right = right;
    }

    public int Value { get; }
    public BinTree? Left { get; }
    public BinTree? Right { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not BinTree other) return false;
        return Value == other.Value
            && Equals(Left, other.Left)
            && Equals(Right, other.Right);
    }

    public override int GetHashCode() =>
        HashCode.Combine(Value, Left, Right);
}

// get data parent
public class Context
{
    public enum Direction {Left, Right}

    public Context(int value, BinTree? sibling, Direction dir, Context? parent)
    {
        Value = value;
        Sibling = sibling;
        Dir = dir;
        Parent = parent;
    }

    public int Value { get; }
    public BinTree? Sibling { get; }
    public Direction Dir { get; }
    public Context? Parent { get; }

}

public class Zipper
{
    private readonly BinTree focus;
    private readonly Context? context;

    private Zipper(BinTree focus, Context? context)
    {
        this.focus = focus;
        this.context = context;
    }

    public static Zipper FromTree(BinTree tree) =>
        new Zipper(tree, null);

    public BinTree ToTree()
    {
        var node = focus;
        var ctx = context;
        while (ctx != null)
        {
            node =ctx.Dir == Context.Direction.Left
                ? new BinTree(ctx.Value, node, ctx.Sibling)
                : new BinTree(ctx.Value, ctx.Sibling, node);
            ctx = ctx.Parent;
        }
        return node;
    }

    public int Value() => focus.Value;


    public Zipper? Left() =>
        focus.Left == null ? null
        : new Zipper(focus.Left,
            new Context(focus.Value, focus.Right, Context.Direction.Left, context));

    public Zipper? Right() =>
       focus.Right == null ? null
        : new Zipper(focus.Right,
            new Context(focus.Value, focus.Left, Context.Direction.Right, context));


    public Zipper? Up()
    {
        if (context == null ) return null;
        var ctx = context;
        var newNode = ctx.Dir == Context.Direction.Left
            ? new BinTree(ctx.Value, focus, ctx.Sibling)
            : new BinTree(ctx.Value, ctx.Sibling, focus);

        return new Zipper(newNode, ctx.Parent);
    }

    public Zipper SetValue (int newValue ) =>
        new Zipper(new BinTree(newValue , focus.Left, focus.Right), context);

    public Zipper SetLeft(BinTree? binTree) =>
        new Zipper(new BinTree(focus.Value , binTree, focus.Right), context);

    public Zipper SetRight(BinTree? binTree) =>
        new Zipper(new BinTree(focus.Value, focus.Left, binTree), context);

    public override bool Equals(object? obj)
    {
        if (obj is not Zipper other) return false;
        return ToTree().Equals(other.ToTree());
    }

    public override int GetHashCode() =>
        ToTree().GetHashCode();
}