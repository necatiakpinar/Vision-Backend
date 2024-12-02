namespace Vision.Application.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}