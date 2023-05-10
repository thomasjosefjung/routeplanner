using graph_oo;

namespace backend.models;

public class Route
{
    public IEnumerable<Node>? Nodes { get; set; }
    public IEnumerable<Edge>? Edges { get; set; }
    public float RouteDistance { get; set; }
    public float StraightLineDistance { get; set; }
}

