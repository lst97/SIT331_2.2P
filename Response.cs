namespace _2._2P;

class Type
{
    public const string RobotCommand = "robot_command";
    public const string RobotMap = "robot_map";
}
interface IResponse<T>
{
    int Code { get; set; }
    string Type { get; set; }
    string Message { get; set; }
    List<T> Data { get; set; }
}

public class Response<T> : IResponse<T>
{
    public int Code { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public List<T> Data { get; set; }


    public Response(int code, String type, string message, List<T> data)
    {
        Code = code;
        Type = type;
        Message = message;
        Data = data;
    }

    public Response(int code, String type, string message, IEnumerable<T> data)
    {
        Code = code;
        Type = type;
        Message = message;
        Data = data.ToList();
    }
}
