using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Mentor.Command.DeleteMentorFollower;

public class DeleteMentorFollowerCommand : IRequest<GenericResponse<DeleteMentorFollowerResponse>>
{
    public string MentorId { get; set; }
    public string FollowerId { get; set; }
    
}