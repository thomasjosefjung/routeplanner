using backend.models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EdgesController : ControllerBase
{
    private readonly ILogger<EdgesController> _logger;

    public EdgesController(ILogger<EdgesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Edge> Get()
    {
        var nodes = backend.bab.Autobahnen.Graph.Nodes.Select(graphNode =>
        {
            return new backend.models.Node(
                graphNode.Name,
                new Coordinates(graphNode.Longitude, graphNode.Latitude)
            );
        });

        return nodes ?? new backend.models.Node[0]; 
    }
}
