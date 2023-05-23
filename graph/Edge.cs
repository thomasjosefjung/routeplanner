using System.Text.Json.Serialization;

namespace graph;

public class Edge
{
    public Vertex From { get; set; }
    public Vertex To { get; set; }
    public float Weight { get; set; }

    public Edge(Vertex from, Vertex to, float weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}
