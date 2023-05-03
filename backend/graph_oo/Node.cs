namespace graph_oo;

public class Node
{
    public string Name { get; set; }

    private List<Edge> _outgoingEdges = new List<Edge>(); 
    public IEnumerable<Edge> OutgoingEdges
    {
        get => _outgoingEdges;  
    }

    public void AddOutgoingEdge(Edge edge)
    {
        _outgoingEdges.Add(edge); 
    }

    public Node(string name)
    {
        Name = name;
    }
}
