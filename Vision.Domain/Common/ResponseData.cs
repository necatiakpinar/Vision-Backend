namespace Vision.Domain.Common;

public class ResponseData<T> where T : class
{
    public T Data { get; set; }
}