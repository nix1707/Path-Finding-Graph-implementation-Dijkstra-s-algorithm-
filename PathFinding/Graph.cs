namespace PathFinding;

internal class DijkstraInfo
{
    public Node Previous = null;
    public int Price;
}

internal class Graph
{
    private readonly List<Node> _nodes = new();

    public IEnumerable<Node> Nodes
    {
        get => _nodes;
    }

    public Node this[string name] => _nodes.Find(n => n.Name == name)!;
    public Node this[int index] => _nodes[index];

    public Graph(params string[] nodeNames)
        => Array.ForEach(nodeNames, AddNode); 

    public void AddNode(string name)
    {
        if (Contains(name) == true)
            return;
        _nodes.Add(new Node(name));
    }

    public void RemoveNode(string name)
        => _nodes.Remove(this[name]);

    public void Display()
    {
        foreach(var node in _nodes)
        {
            Console.WriteLine("Node: " + node.Name);
            Console.WriteLine("Incident nodes:" + string.Join(',', node.IncidentNodes) + "\n");
        }
    }

    public void ConnectMany(params(string from, string to, int distance)[] connectedInfos)
    {
        foreach(var (from, to, distance) in connectedInfos)
            Connect(from, to, distance);
    }
    
    public void Connect(string from, string to, int distance)
        => this[from].Connect(this[to], distance);

    public void Disconnect(string from, string to)
        => this[from].Disconnect(this[to]);

    public bool Contains(string nodeName)
        => Contains(this[nodeName]);

    public bool Contains(Node node)
        => _nodes.Contains(node);

    public static List<Node> FindShortest(Graph graph, Node start, Node end)
    {
        Node? toConsider = null;
        var unvisited = graph.Nodes.ToList();
        var passed = new Dictionary<Node, DijkstraInfo>()
        {
            [start] = new DijkstraInfo() { Price = 0 }
        };

        while (toConsider != end)
        {
            //Find nearest node with the best price
            foreach (var node in unvisited)
            {
                var bestPrice = int.MaxValue;
                if (passed.ContainsKey(node) == false || passed[node].Price > bestPrice)
                    continue;
                toConsider = node;
                bestPrice = passed[node].Price;
            }

            if (toConsider == null)
                return null;

            //Set sum to next node
            foreach (var edge in toConsider.IncidentEdges.Where(e => toConsider == e.From))
            {
                var nextNode = edge.GetIncident(toConsider);
                var currentPrice = edge.Distance + passed[toConsider].Price;

                if (passed.ContainsKey(nextNode) == false || passed[nextNode].Price > currentPrice)
                {
                    passed[nextNode] = new DijkstraInfo() { Previous = toConsider, Price = currentPrice };
                }
            }
            unvisited.Remove(toConsider);

        }
        return GetPath(passed,end);
    }

    private static List<Node> GetPath(Dictionary<Node,DijkstraInfo> track,Node end)
    {
        var path = new List<Node>();
        while (end != null)
        {
            path.Add(end);
            end = track[end].Previous;
        }
        path.Reverse();
        return path;
    }
}
