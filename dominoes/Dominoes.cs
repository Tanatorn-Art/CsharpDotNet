using System;
using System.Collections.Generic;
using System.Linq;

public record Domino(int Left, int Right)
{
    public Domino Flipped() => new Domino(Right, Left);
}

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        if (!dominoes.Any())
            return true;

        var dominoList = dominoes.Select(d => new Domino(d.Item1, d.Item2)).ToList();

        var graph = new Dictionary<int, List<Domino>>();
        foreach (var d in dominoList)
        {
            if (!graph.ContainsKey(d.Left)) graph[d.Left] = new List<Domino>();
            if (!graph.ContainsKey(d.Right)) graph[d.Right] = new List<Domino>();

            graph[d.Left].Add(d);
            graph[d.Right].Add(d.Flipped());
        }

        int start = dominoList.First().Left;

        var stack = new Stack<int>();
        var path = new List<int>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            int v = stack.Peek();
            if (graph[v].Any())
            {
                var edge = graph[v].First();
                graph[v].Remove(edge);
                int u = edge.Right;
                graph[u].Remove(edge.Flipped());
                stack.Push(u);
            }
            else
            {
                path.Add(stack.Pop());
            }
        }

        path.Reverse();

        var chain = new List<Domino>();
        for (int i = 0; i < path.Count - 1; i++)
            chain.Add(new Domino(path[i], path[i + 1]));

        return chain.Count == dominoList.Count && path.First() == path.Last();
    }
}
