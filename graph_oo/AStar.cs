using System;
using System.Collections.Generic;

namespace graph_oo
{
    public class AStar
    {
        public static (List<Edge>, HashSet<Node>) FindShortestPath(Graph graph, Node start, Node goal)
        {
            var closedSet = new HashSet<Node>();
            var openSet = new HashSet<Node> { start };
            var cameFrom = new Dictionary<Node, Edge>();
            var gScore = new Dictionary<Node, float>();
            var fScore = new Dictionary<Node, float>();

            // Initialize start node
            gScore[start] = 0;
            fScore[start] = start.GetDistanceTo(goal);

            var touchedNodes = new HashSet<Node>(); 

            while (openSet.Count > 0)
            {
                // Get the node in openSet with lowest fScore
                Node? current = null;
                float lowestScore = float.MaxValue;
                foreach (var node in openSet)
                {
                    if (fScore.ContainsKey(node) && fScore[node] < lowestScore)
                    {
                        current = node;
                        lowestScore = fScore[node];
                    }
                }

                touchedNodes.Add(current); 

                if (current == goal)
                {
                    var path = ReconstructPath(cameFrom, current);
                    return (path, touchedNodes); 
                }

                if (current == null)
                {
                    return (new List<Edge>(), touchedNodes); 
                }

                openSet.Remove(current);
                closedSet.Add(current);

                // Check outgoing edges
                foreach (var edge in current.OutgoingEdges)
                {
                    var neighbor = edge.To;

                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    float tentativeGScore = gScore[current] + edge.Weight;
                    bool tentativeIsBetter;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                        tentativeIsBetter = true;
                    }
                    else if (tentativeGScore < gScore[neighbor])
                    {
                        tentativeIsBetter = true;
                    }
                    else
                    {
                        tentativeIsBetter = false;
                    }

                    if (tentativeIsBetter)
                    {
                        cameFrom[neighbor] = edge;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + neighbor.GetDistanceTo(goal);
                    }
                }
            }

            // No path found
            return (new List<Edge>(), touchedNodes); 
        }

        private static List<Edge> ReconstructPath(Dictionary<Node, Edge> cameFrom, Node current)
        {
            var path = new List<Edge> { };

            while (cameFrom.ContainsKey(current))
            {
                var edge = cameFrom[current];
                path.Insert(0, edge);
                current = edge.From;
            }

            return path;
        }
    }
}