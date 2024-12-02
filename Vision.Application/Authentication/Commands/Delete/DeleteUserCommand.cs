using MediatR;
using MongoDB.Bson;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Authentication.Commands.Delete;

public class DeleteUserCommand : IRequest<GenericResponse<DeleteUserResponse>>
{
    public string UserId { get; set; }
}