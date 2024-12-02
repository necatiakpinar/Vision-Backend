using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<GenericResponse<ForgotPasswordResponse>>
{
    
}