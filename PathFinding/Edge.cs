namespace PathFinding;

internal class Edge
{
    public readonly Node From;
    public readonly Node To;

    public int Distance;

    public Edge(Node from, Node to, int distance)
    {
        From = from;
        To = to;
        Distance = distance;
    }

    public Node GetIncident(Node node)
    {
        if(node.Equals(From) || node.Equals(To))
            return node.Equals(From) ? To : From;
        throw new ArgumentException("Given node is not incident to any of the nodes");
    }
}
