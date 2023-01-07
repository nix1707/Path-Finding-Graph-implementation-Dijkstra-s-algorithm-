namespace PathFinding;

internal class Node
{
    public readonly string Name;
    public readonly List<Edge> _edges = new();

    public IEnumerable<Node> IncidentNodes
    {
        get => _edges.Select(e => e.GetIncident(this));
    }

    public IEnumerable<Edge> IncidentEdges
    {
        get => _edges;
    }

    public Node(string name)
       => Name = name;

    public override string ToString() => Name;

    public void Connect(Node connected, int distance)
    {
        if (IncidentNodes.Contains(connected) == true)
            return;
        _edges.Add(new Edge(this, connected, distance));
    }

    public void Disconnect(Node connected)
    {
        var edge = _edges.Find(e => e.From.Equals(this) && e.To.Equals(connected));
        _edges.Remove(edge);
    }
    
}
