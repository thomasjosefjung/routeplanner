namespace graph_oo;

public class Graph
{
    // public List<Node> GetNodesFlatClone()
    // {
    //     return new List<Node>(_nodes.Values); 
    // }

    // public List<Edge> GetEdgesFlatClone()
    // {
    //     return new List<Edge>(_edges); 
    // }

    public Node FindNode(string id)
    {
        return _nodes[id]; 
    }

    private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
    public IEnumerable<Node> Nodes
    {
        get => _nodes.Values;
    }
    private List<Edge> _edges = new List<Edge>();
    public IEnumerable<Edge> Edges
    {
        get => _edges;
    }

    public void AddNode(Node node)
    {
        _nodes.Add(node.Name, node);
    }

    public void AddEdge(Edge edge)
    {
        _edges.Add(edge);
    }
}
