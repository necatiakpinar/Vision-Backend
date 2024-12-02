using Vision.Application.Authentication.Common.Interfaces;
using Vision.Application.Common;


namespace Vision.Application.Authentication.Common;

[Serializable]
public class GenericResponse<T> where T : IResponse
{
    public T? Data { get; set; }
    public WarningResult? WarningResult { get; set; }
}