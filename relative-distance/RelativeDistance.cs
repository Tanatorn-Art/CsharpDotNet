using System;
using System.Collections.Generic;
using System.Linq;

public class RelativeDistance
{
    // We'll build a bidirectional graph to represent relationships
    private Dictionary<string, List<string>> _relationships;

    public RelativeDistance(Dictionary<string, string[]> familyTree)
    {
        // Initialize the relationships graph
        _relationships = new Dictionary<string, List<string>>();

        // Process the family tree to build bidirectional relationships
        foreach (var entry in familyTree)
        {
            string parent = entry.Key;
            string[] children = entry.Value;

            // Ensure parent exists in relationships
            if (!_relationships.ContainsKey(parent))
            {
                _relationships[parent] = new List<string>();
            }

            // Add each child as related to parent
            foreach (string child in children)
            {
                // Add child to parent's relationships
                _relationships[parent].Add(child);

                // Add parent to child's relationships (bidirectional)
                if (!_relationships.ContainsKey(child))
                {
                    _relationships[child] = new List<string>();
                }
                _relationships[child].Add(parent);
            }

            // Create sibling relationships
            // For each pair of children, add them as related to each other
            for (int i = 0; i < children.Length; i++)
            {
                for (int j = i + 1; j < children.Length; j++)
                {
                    string child1 = children[i];
                    string child2 = children[j];

                    if (!_relationships.ContainsKey(child1))
                    {
                        _relationships[child1] = new List<string>();
                    }
                    if (!_relationships.ContainsKey(child2))
                    {
                        _relationships[child2] = new List<string>();
                    }

                    // Add sibling relationships
                    _relationships[child1].Add(child2);
                    _relationships[child2].Add(child1);
                }
            }
        }
    }

    public int DegreeOfSeparation(string personA, string personB)
    {
        // If either person doesn't exist in our data, they're not related
        if (!_relationships.ContainsKey(personA) || !_relationships.ContainsKey(personB))
        {
            return -1;
        }

        // Special case: same person
        if (personA == personB)
        {
            return 0;
        }

        // Use breadth-first search to find shortest path
        Queue<string> queue = new Queue<string>();
        Dictionary<string, int> distances = new Dictionary<string, int>();
        HashSet<string> visited = new HashSet<string>();

        // Start from personA
        queue.Enqueue(personA);
        distances[personA] = 0;
        visited.Add(personA);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            int currentDistance = distances[current];

            // Check all related individuals
            foreach (string related in _relationships[current])
            {
                // If we found personB, return the distance
                if (related == personB)
                {
                    return currentDistance + 1;
                }

                // If we haven't visited this person yet, add them to the queue
                if (!visited.Contains(related))
                {
                    queue.Enqueue(related);
                    distances[related] = currentDistance + 1;
                    visited.Add(related);
                }
            }
        }

        // If we've exhausted the search and didn't find personB, they're not related
        return -1;
    }
}