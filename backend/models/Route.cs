using graph_oo;

namespace backend.models;

public class Route
{
    public IEnumerable<string>? Nodes { get; set; }
    public float RouteDistance { get; set; }
    public float StraightLineDistance { get; set; }
}

