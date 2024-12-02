using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Authentication.Commands.Delete;

public class DeleteUserResponse : IResponse
{
    public bool Succeeded { get; set; }
}