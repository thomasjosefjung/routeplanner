namespace graph_oo;

public class Dijkstra
{
    public static List<Edge> FindShortestPath(
        Graph graph,
        Node from,
        Node To)
    {
        Dictionary<Node, Edge?> predecessor = new Dictionary<Node, Edge?>();
        Dictionary<Node, float> distanceMap = new Dictionary<Node, float>();

        foreach (var node in graph.Nodes)
        {
            predecessor.Add(node, null);
            distanceMap.Add(node, float.MaxValue);
        }

        distanceMap[from] = 0.0f; 

        bool foundTarget = false; 

        while (distanceMap.Count > 0 && !foundTarget)
        {
            (Node node, float dist) = GetLeastDistanceNode(distanceMap);
            distanceMap.Remove(node);

            foreach (var edge in node.OutgoingEdges)
            {
                if (distanceMap.ContainsKey(edge.To))
                {
                    float abstandUeberDiesenWeg = dist + edge.Weight; 

                    if (abstandUeberDiesenWeg < distanceMap[edge.To])
                    {
                        predecessor[edge.To] = edge; 
                        distanceMap[edge.To] = dist + edge.Weight; 
                    }

                    if (edge.To == To)
                    {
                        foundTarget = true; 
                    }
                }
            }
        }

        var result = new List<Edge>();
        Edge? next = predecessor[To];

        // erstelle pfad: 
        while (next != null)
        {
            result.Insert(0, next); 
            next = predecessor[next.From]; 
        }

        return result;
    }

    static (Node, float) GetLeastDistanceNode(Dictionary<Node, float> distanceMap)
    {
        Node? currentLeastNode = null;
        float currentLeastDist = float.MaxValue;

        foreach (var kvp in distanceMap)
        {
            if (kvp.Value <= currentLeastDist)
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