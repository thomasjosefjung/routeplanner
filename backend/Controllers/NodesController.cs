using backend.bab;
using graph_oo;
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
    public IEnumerable<string> Get()
    {
        IEnumerable<string> nodeNames = 
            from node in Autobahnen.Graph.Nodes
            select node.Name; 

        return nodeNames ?? new string[0]; 
    }
}
