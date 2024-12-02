using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Authentication.Commands.Register;

public record RegisterCommand(string UserName, string FirstName, string LastName, string Email, string Password) : IRequest<GenericResponse<RegisterResponse>>;