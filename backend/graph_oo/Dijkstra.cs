namespace graph_oo;

public class Dijkstra
{
    public static List<Node> GetShortestPath(
        Graph graph,
        Node from,
        Node To)
    {
        Dictionary<Node, Node?> predecessor = new Dictionary<Node, Node?>();
        Dictionary<Node, float> distanceMap = new Dictionary<Node, float>();

        foreach (var node in graph.Nodes)
        {
            predecessor.Add(node, null);
            distanceMap.Add(node, float.MaxValue);
        }

        while (distanceMap.Count > 0)
        {
            (Node node, float dist) = GetLeastDistanceNode(distanceMap);
            distanceMap.Remove(node);

            foreach (var edge in node.OutgoingEdges)
            {
                predecessor[edge.To] = node; 
                distanceMap[edge.To] = dist + edge.Weight; 
            }
        }

        var result = new List<Node>();
        result.Prepend(To);
        Node? next = To;

        // erstelle pfad: 
        while (next != null)
        {
            result.Prepend(next); 
            next = predecessor[next]; 
        }

        return result;
    }

    static (Node, float) GetLeastDistanceNode(Dictionary<Node, float> distanceMap)
    {
        Node? currentLeastNode = null;
        float currentLeastDist = float.MaxValue;

        foreach (var kvp in distanceMap)
        {
            if (kvp.Value < currentLeastDist)
            {
                currentLeastNode = kvp.Key;
                currentLeastDist = kvp.Value;
            }
        }

        _ = currentLeastNode ?? throw new Exception();

        return (currentLeastNode, currentLeastDist);
    }


}

public class NodeComparer : IComparer<float>
{
    public int Compare(float prio1, float prio2)
    {
        if (prio1 < prio2)
        {
            return 1;
        }

        if (prio1 == prio2)
        {
            return 0;
        }

        return -1;
    }
}