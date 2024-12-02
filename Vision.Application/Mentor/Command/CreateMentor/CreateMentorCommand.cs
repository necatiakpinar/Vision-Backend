using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Mentor.Command.CreateMentor;

public class CreateMentorCommand : IRequest<GenericResponse<CreateMentorResponse>>
{
    public string UserId { get; set; }
}