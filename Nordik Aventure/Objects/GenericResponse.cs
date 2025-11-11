namespace GestBibli.Objects;

public class GenericResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    
    public int Code { get; set; }

    public GenericResponse() { }

    public GenericResponse(T data, string? message = null)
    {
        Success = true;
        Data = data;
        Message = message;
        Code = 200;
    }

    public GenericResponse(string message, int code)
    {
        Success = false;
        Message = message;
        Code = code;
    }
}