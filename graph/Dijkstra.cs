namespace graph;

public class Dijkstra
{
    public static (List<Edge>, IEnumerable<Vertex>) FindShortestPath(
        Graph graph,
        Vertex from,
        Vertex To)
    {
        var unhandledNodes = new HashSet<Vertex>(); 

        foreach (var node in graph.Nodes)
        {
            node.Predecessor = null; 
            unhandledNodes.Add(node); 
            node.CurrentDistance = float.MaxValue; 
        }

        from.CurrentDistance = 0.0f; 

        var touchedNodes = new List<Vertex>(); 

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

    static Vertex GetLeastDistanceNode(HashSet<Vertex> unhandledNodes)
    {
        return unhandledNodes.MinBy<Vertex, float>((Vertex node) => node.CurrentDistance) ?? throw new Exception("cannot happen"); 
    }
}
