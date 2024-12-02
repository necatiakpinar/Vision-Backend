using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Mentor.Command.DeleteMentor;

public class DeleteMentorCommand : IRequest<GenericResponse<DeleteMentorResponse>>
{
    public string MentorId { get; set; }
}