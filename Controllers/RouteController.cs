using backend.bab;
using graph_oo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;

    public RouteController(ILogger<RouteController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public models.Route Get(string from, string to)
    {
        var nodeFrom = Autobahnen.Graph.FindNode(from); 
        var nodeTo = Autobahnen.Graph.FindNode(to); 

        var started = DateTime.Now; 
        var path = AStar.FindShortestPath(
            Autobahnen.Graph, nodeFrom, nodeTo); 

        // var path = AStar.FindShortestPath(
        //     Autobahnen.Graph, nodeFrom, nodeTo); 

        var computationTime = (DateTime.Now - started).TotalMilliseconds; 

        _ = path ?? throw new NullReferenceException();

        var edges = path.Select(edge =>
        {
            return new models.Edge
            {
                CoordsFrom = new models.Coordinates(edge.From.Longitude, edge.From.Latitude),
                CoordsTo = new models.Coordinates(edge.To.Longitude, edge.To.Latitude)
            };
        });

        var nodes = path.Select(edge =>
        {
            return new models.Node(
                edge.To.Name,
                new models.Coordinates(edge.From.Longitude, edge.To.Latitude));
        });

        nodes.Prepend(new models.Node(
                nodeFrom.Name,
                new models.Coordinates(nodeFrom.Longitude, nodeFrom.Latitude)));

        float linearDistance = nodeTo.GetDistanceTo(nodeFrom); 

        float routeDistance = path.Select(edge => edge.Weight).Sum(); 

        return new models.Route
        {
            Nodes = nodes, 
            Edges = edges, 
            StraightLineDistance = linearDistance, 
            RouteDistance = routeDistance,  
            ComputationTime = (float)computationTime
        };
    }
}
