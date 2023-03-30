interface IMap
{
    int Id { get; }
    Cordinate Size { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
    DateTime CreatedDate { get; }
    DateTime ModifiedDate { get; }
}

public class Cordinate
{
    public int X { get; set; }
    public int Y { get; set; }
    public Cordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Map : IMap
{
    public int Id { get; }
    public Cordinate Size { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; }
    public DateTime ModifiedDate { get; set; }

    public Map(int id, int columns, int rows, string name, string? description, DateTime createdDate, DateTime modifiedDate)
    {
        Id = id;
        Size = new Cordinate(columns, rows);
        Name = name;
        Description = description;
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
    }
}
