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

        var path = Dijkstra.GetShortestPath(
            Autobahnen.Graph, nodeFrom, nodeTo); 

        _ = path ?? throw new NullReferenceException(); 


        float linearDistance = nodeTo.GetDistanceTo(nodeFrom); 

        IEnumerable<string> nodeNames = path.Select(edge => edge.To.Name); 
        nodeNames = nodeNames.Prepend(nodeFrom.Name); 

        // IEnumerable<string> nodeNames = 
        //     from edge in path
        //     select edge.To.Name; 

        float routeDistance = path.Select(edge => edge.Weight).Sum(); 

        // float routeDistance = 
        //     (from edge in path
        //     select edge.Weight).Sum(); 


        return new models.Route
        {
            Nodes = nodeNames, 
            StraightLineDistance = linearDistance, 
            RouteDistance = routeDistance
        };
    }
}
