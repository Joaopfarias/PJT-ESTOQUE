using System.Collections.Generic;

public class ResponseService<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public T Data { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }
}