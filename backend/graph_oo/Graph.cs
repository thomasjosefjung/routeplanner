namespace graph_oo;

public class Graph
{
    public List<Node> GetNodesFlatClone()
    {
        return new List<Node>(_nodes); 
    }

    public List<Edge> GetEdgesFlatClone()
    {
        return new List<Edge>(_edges); 
    }

    private List<Node> _nodes = new List<Node>();
    public IEnumerable<Node> Nodes
    {
        get => _nodes;
    }
    private List<Edge> _edges = new List<Edge>();
    public IEnumerable<Edge> Edges
    {
        get => _edges;
    }

    public void AddNode(Node node)
    {
        _nodes.Add(node);
    }

    public void AddEdge(Edge edge)
    {
        _edges.Add(edge);
    }
}
