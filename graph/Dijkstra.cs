namespace graph;

public class Dijkstra
{
    public static (List<Edge>, HashSet<Node>) FindShortestPath(
        Graph graph,
        Node from,
        Node To)
    {
        var unhandledNodes = new HashSet<Node>(); 

        foreach (var node in graph.Nodes)
        {
            node.Predecessor = null; 
            unhandledNodes.Add(node); 
            node.CurrentDistance = float.MaxValue; 
        }

        from.CurrentDistance = 0.0f; 

        var touchedNodes = new HashSet<Node>(); 

        while (unhandledNodes.Count > 0)
        {
            var node = GetLeastDistanceNode(unhandledNodes); 
            unhandledNodes.Remove(node); 

            touchedNodes.Add(node); 

            if (node == To)
            {
                break; 
            }

            foreach (var edge in node.OutgoingEdges)
            {
                if (unhandledNodes.Contains(edge.To))
                {
                    float distanceCandidate = node.CurrentDistance + edge.Weight; 

                    if (distanceCandidate < edge.To.CurrentDistance)
                    {
                        // predecessor[edge.To] = edge; 
                        edge.To.Predecessor = edge; 

                        // distanceMap[edge.To] = dist + edge.Weight; 
                        edge.To.CurrentDistance = node.CurrentDistance + edge.Weight;
                    }
                }
            }
        }

        var result = new List<Edge>();
        var next = To.Predecessor; 

        // erstelle pfad: 
        while (next != null)
        {
            result.Insert(0,next); 
            next = next.From.Predecessor; 
        }

        return (result, touchedNodes);
    }

    static Node GetLeastDistanceNode(HashSet<Node> unhandledNodes)
    {
        return unhandledNodes.MinBy<Node, float>((Node node) => node.CurrentDistance) ?? throw new Exception("cannot happen"); 
    }
}
