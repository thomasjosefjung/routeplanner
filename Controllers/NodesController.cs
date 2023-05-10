using backend.models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NodesController : ControllerBase
{
    private readonly ILogger<NodesController> _logger;

    public NodesController(ILogger<NodesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Node> Get()
    {
        var nodes = backend.bab.Autobahnen.Graph.Nodes.Select(graphNode =>
        {
            return new backend.models.Node(
                graphNode.Name,
                new Coordinates(graphNode.Longitude, graphNode.Latitude)
            );
        }).OrderBy(node => {
            return node.Name;
        });

        return nodes; 
    }
}
