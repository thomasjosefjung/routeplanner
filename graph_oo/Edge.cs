namespace graph_oo;

public class Edge
{
    public Node From { get; set; }
    public Node To { get; set; }
    public float Weight { get; set; }

    public Edge(Node from, Node to, float weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}
