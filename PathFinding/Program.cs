namespace PathFinding;

public class Program
{
    static void Main()
    {
        Graph graph = new("A", "B", "C", "D", "E", "F");

        graph.ConnectMany
        (
            ("A", "E", 12),
            ("A", "F", 2),
            ("B", "D", 4),
            ("B", "C", 5),
            ("D", "E", 7),
            ("C", "E", 8),
            ("F", "E", 3),
            ("D", "A", 6),
            ("F", "C", 9)
        );

        Console.WriteLine("===== Graph =====");

        graph.Display();

        var path = Graph.FindShortest(graph,graph["A"], graph["E"]);
        Console.WriteLine("#Shortest path from - {0} to - {1}\nis {2}",
            "A","E",string.Join('>',path));
    }
}