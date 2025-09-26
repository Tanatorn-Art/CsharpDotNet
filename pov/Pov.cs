using System;
using System.Collections.Generic;
public class Tree
{
    internal string value;
    internal List<Tree> children;

    public Tree(string value, params Tree[] children)
    {
        this.value = value;
        this.children = new List<Tree>(children);
    }
    internal List<Tree> FindPath(string node)
    {
        if (value == node)
            return new List<Tree>{this};
        foreach (Tree child in children)
        {
            List<Tree> path = child.FindPath(node);
            if (path != null)
            {
                path.Add(this);
                return path;
            }
        }
        return null;
    }
    public override bool Equals(object o)
    {
        if (o is Tree other)
        {
            if (value != other.value || children.Count != other.children.Count)
                return false;
            children.Sort((x, y) => x.value.CompareTo(y.value));
            other.children.Sort((x, y) => x.value.CompareTo(y.value));
            for (int i = 0; i < children.Count; i++)
                if (!children[i].Equals(other.children[i]))
                    return false;
            return true;
        }
        return false;
    }
}
public static class Pov
{
    public static Tree FromPov(Tree tree, string from)
    {
        List<Tree> path = tree.FindPath(from);
        if (path == null)
            throw new ArgumentException();
        for (int i = 1; i < path.Count; i++)
        {
            path[i].children.Remove(path[i - 1]);
            path[i - 1].children.Add(path[i]);
        }
        return path[0];
    }
    public static IEnumerable<string> PathTo(string from, string to, Tree tree)
    {
        List<Tree> path1 = tree.FindPath(from);
        List<Tree> path2 = tree.FindPath(to);
        if (path1 == null || path2 == null)
            throw new ArgumentException();
        Tree parent = null;
        while (path1.Count > 0 && path2.Count > 0)
        {
            if (path1[path1.Count - 1].value != path2[path2.Count - 1].value)
                break;
            parent = path1[path1.Count - 1];
            path1.RemoveAt(path1.Count - 1);
            path2.RemoveAt(path2.Count - 1);
        }
        List<string> result = new List<String>();
        foreach (Tree node in path1)
            result.Add(node.value);
        result.Add(parent.value);
        path2.Reverse();
        foreach (Tree node in path2)
            result.Add(node.value);
        return result;
    }
}