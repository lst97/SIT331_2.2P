using Microsoft.AspNetCore.Mvc;

namespace _2._2P.Controllers;

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{

    private static readonly List<RobotCommand> _commands = new
        List<RobotCommand>
    {

        // Legacy: moveForward(), rotate(int degrees)
        new RobotCommand(1, "LEFT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(2, "RIGHT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(3, "MOVE", true, DateTime.Now, DateTime.Now),

        // Legacy: robot.commissioning()
        new RobotCommand(4, "PLACE", false, DateTime.Now, DateTime.Now),
        
        // Legacy: robot.operation()
        new RobotCommand(5, "REPORT", false, DateTime.Now, DateTime.Now)

    };
    private readonly ILogger<RobotCommandsController> _logger;

    public RobotCommandsController(ILogger<RobotCommandsController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public Response<RobotCommand> GetAllRobotCommands()
    {
        return new Response<RobotCommand>(200, Type.RobotCommand, "SUCCESS", _commands);
    }

    [HttpGet("move")]
    public Response<RobotCommand> GetMoveCommandsOnly()
    {
        // return a filtered _commands field here after you filter out
        // non - move commands.
        // you might find LINQ Where clause helpful for this matter.

        return new Response<RobotCommand>(200, Type.RobotCommand, "SUCCESS", _commands.Where(x => x.IsMoveCommand == true));
    }

    [HttpGet("{id}", Name = "GetRobotCommand")]
    public IActionResult GetRobotCommandById(int id)
    {
        // Check whether you have a command with a specified Id and return
        RobotCommand command = _commands.Find(x => x.Id == id);
        if (command == null)
        {
            return NotFound();
        }
        return Ok(command);
    }

    [HttpPost()]
    public IActionResult AddRobotCommand(RobotCommand newCommand)
    {
        if (newCommand == null)
        {
            return BadRequest();
        }

        // find if the name of the new command already exists in the list
        RobotCommand command = _commands.Find(command => command.Name == newCommand.Name);
        if (command != null)
        {
            return Conflict();
        }

        newCommand = new RobotCommand(
            _commands.Count + 1,
            newCommand.Name,
            newCommand.IsMoveCommand,
            DateTime.Now,
            DateTime.Now,
            newCommand.Description
        );
        _commands.Add(newCommand);

        return CreatedAtRoute("GetRobotCommand", new { id = newCommand.Id }, newCommand);

    }

    [HttpPut("{id}")]
    public IActionResult UpdateRobotCommand(int id, RobotCommand updatedCommand)
    {
        if (updatedCommand == null || updatedCommand.Name == "")
        {
            return BadRequest();
        }

        RobotCommand command = _commands.Find(command => command.Id == id);
        int index = _commands.FindIndex(command => command.Id == id);

        if (command == null)
        {
            return NotFound();
        }

        // check if the name of the updated command already exists in the list
        if (_commands.Find(command => command.Name == updatedCommand.Name) != null)
        {
            return Conflict();
        }

        command.Name = updatedCommand.Name;
        command.ModifiedDate = DateTime.Now;
        command.IsMoveCommand = updatedCommand.IsMoveCommand;
        command.Description = updatedCommand.Description;

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRobotCommand(int id)
    {
        RobotCommand command = _commands.Find(command => command.Id == id);
        if (command == null)
        {
            return NotFound();
        }

        _commands.Remove(command);
        return Ok();
    }
}
