using System.Numerics;

using System.Text.Json.Serialization;

namespace graph_oo;



public class Node
{
    public string Name { get; set; }

    public float Latitude { get; set; }
    public float Longitude { get; set; }

    internal float Distance { get; set; }
    internal Edge? Predecessor { get; set; }

    private List<Edge> _outgoingEdges = new List<Edge>();
    [JsonIgnore]
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

    public float GetDistanceTo(Node n2)
    {
        float lonA = this.Longitude * MathF.PI / 180.0f;
        float latA = this.Latitude * MathF.PI / 180.0f;

        float lonB = n2.Longitude * MathF.PI / 180.0f;
        float latB = n2.Latitude * MathF.PI / 180.0f;

        return 6371 * (MathF.Acos(MathF.Sin(latA) * MathF.Sin(latB) + MathF.Cos(latA) * MathF.Cos(latB) * MathF.Cos(lonA - lonB)));
    }
}
