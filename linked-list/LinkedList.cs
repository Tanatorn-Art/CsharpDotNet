public class Deque<T>
{
    private class Node
    {
        public T Value;
        public Node Prev;
        public Node Next;
        public Node(T value) => Value = value;
    }

    private Node head;
    private Node tail;

    public void Push(T value)
    {
        var node = new Node(value);
        if (tail == null) // is empty
        {
            head = tail = node;
        }
        else 
        {
            tail.Next = node;
            node.Prev = tail;
            tail = node;
        }
    }

    // remove the last node
    public T Pop()
    {
        if (tail == null) throw new InvalidOperationException("List is empty");

        var value = tail.Value;
        tail = tail.Prev;
        if (tail == null)
            head = null; // list was empty
        else
            tail.Next = null;

        return value;
    }

    // add a node to the beginning
    public void Unshift(T value)
    {
        var node = new Node(value);
        if (head == null) // is empty
        {
            head = tail = node;
        }
        else
        {
            node.Next = head;
            head.Prev = node;
            head = node;
        }
    }

    // remove the first node
    public T Shift()
    {
        if (head == null) throw new InvalidOperationException("Deque is empty");

        var value = head.Value;
        head = head.Next;
        if (head == null)
            tail = null; // list was empty
        else
            head.Prev = null;

        return value;
    }
}