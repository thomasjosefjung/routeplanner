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
        var nodes = backend.bab.Autobahnen.Graph.Edges.Select(edge =>
        {
            return new backend.models.Edge
            {
                CoordsFrom = new Coordinates(edge.From.Longitude, edge.From.Latitude), 
                CoordsTo = new Coordinates(edge.To.Longitude, edge.To.Latitude)
            };
        });

        return  nodes ?? new backend.models.Edge[0]; 
    }
}
