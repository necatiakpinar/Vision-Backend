using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<GenericResponse<LoginResponse>>;