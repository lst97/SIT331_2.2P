using Microsoft.AspNetCore.Mvc;

namespace _2._2P.Controllers;

[ApiController]
[Route("api/maps")]
public class MapController : ControllerBase
{
    private static readonly List<Map> _map = new
    List<Map>
    {
        new Map(0, 5, 5, "test_map_1", "", DateTime.Now, DateTime.Now),
        new Map(1, 5, 5, "test_map_2", "", DateTime.Now, DateTime.Now),
        new Map(2, 5, 5, "test_map_3", "", DateTime.Now, DateTime.Now)
    };

    private readonly ILogger<MapController> _logger;

    public MapController(ILogger<MapController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public Response<Map> GetAllMaps()
    {
        return new Response<Map>(200, Type.RobotMap, "SUCCESS", _map);
    }

    [HttpGet("square")]
    public Response<Map> GetSquareMaps()
    {
        return new Response<Map>(200, Type.RobotMap, "SUCCESS", _map.Where(map => map.Size.X == map.Size.Y));
    }

    [HttpGet("{id}", Name = "GetMap")]
    public IActionResult GetMapById(int id)
    {
        Map map = _map.Find(map => map.Id == id);
        if (map == null)
        {
            return NotFound();
        }
        return Ok(map);
    }

    [HttpPost()]
    public IActionResult AddMap(Map newMap)
    {
        if (newMap == null)
        {
            return BadRequest();
        }

        // find if the name of the new command already exists in the list
        Map map = _map.Find(command => command.Name == newMap.Name);
        if (map != null)
        {
            return Conflict();
        }

        newMap = new Map(
            _map.Count + 1,
            newMap.Size.X,
            newMap.Size.Y,
            newMap.Name,
            newMap.Description,
            DateTime.Now,
            DateTime.Now
        );
        _map.Add(newMap);

        return CreatedAtRoute("GetRobotCommand", new { id = newMap.Id }, newMap);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMap(int id, Map updatedMap)
    {
        if (updatedMap == null || updatedMap.Name == "")
        {
            return BadRequest();
        }

        Map map = _map.Find(map => map.Id == id);
        int index = _map.FindIndex(map => map.Id == id);

        if (map == null)
        {
            return NotFound();
        }

        // check if the name of the updated command already exists in the list
        if (_map.Find(map => map.Name == updatedMap.Name) != null)
        {
            return Conflict();
        }

        map.Name = updatedMap.Name;
        map.ModifiedDate = DateTime.Now;
        map.Description = updatedMap.Description;
        map.Size = updatedMap.Size;


        return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteMap(int id)
    {
        Map map = _map.Find(map => map.Id == id);
        if (map == null)
        {
            return NotFound();
        }

        _map.Remove(map);
        return Ok();
    }

    [HttpGet("{id}/{x}-{y}")]
    public IActionResult CheckCoordinate(int id, int x, int y)
    {
        // check if the coordinate is valid
        if (x <= 0 || y <= 0)
        {
            return BadRequest();
        }
        Map map = _map.Find(command => command.Id == id);
        if (map == null)
        {
            return NotFound();
        }

        int mapX = map.Size.X;
        int mapY = map.Size.Y;

        bool isOnMap = x <= mapX && y <= mapY;

        return Ok(isOnMap);
    }
}
