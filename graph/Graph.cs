using System.Text.Json.Serialization;

namespace graph;

public class Graph
{
    public Vertex FindNode(string id)
    {
        return _nodes[id]; 
    }

    private Dictionary<string, Vertex> _nodes = new Dictionary<string, Vertex>();
    public IEnumerable<Vertex> Nodes
    {
        get => _nodes.Values;
        set
        {
            foreach(Vertex n in value)
            {
                this.AddNode(n); 
            }
        }
    }
    private List<Edge> _edges = new List<Edge>();
    public IEnumerable<Edge> Edges
    {
        get => _edges;
        set => _edges = value.ToList(); 
    }

    public void AddNode(Vertex node)
    {
        _nodes.Add(node.Name, node);
    }

    public void AddEdge(Edge edge)
    {
        _edges.Add(edge);
    }

    public void WriteToFile(string filename)
    {
        File.WriteAllText(filename, System.Text.Json.JsonSerializer.Serialize(this)); 
    }

    public static Graph FromJsonFile(string filename)
    {
        string json = File.ReadAllText(filename); 
        return System.Text.Json.JsonSerializer.Deserialize<Graph>(json); 
    }

    // private void CompileOutgoingEdges()
    // {
    //     foreach(var e in Edges)
    //     {
    //         var nodeFrom = Nodes[e.From]
    //     }
    // }
}
