interface IRobotCommand
{
    int Id { get; }
    string Name { get; set; }
    bool IsMoveCommand { get; set; }
    DateTime CreatedDate { get; }
    DateTime ModifiedDate { get; }
    string? Description { get; set; }
}
public class RobotCommand : IRobotCommand
{
    public int Id { get; }
    public string Name { get; set; }
    public bool IsMoveCommand { get; set; }
    public DateTime CreatedDate { get; }
    public DateTime ModifiedDate { get; set; }
    public string? Description { get; set; }
    public RobotCommand(
        int id, string name, bool isMoveCommand, DateTime createdDate,
        DateTime modifiedDate, string? description = null)
    {
        Id = id;
        Name = name;
        IsMoveCommand = isMoveCommand;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        Description = description;
    }
}