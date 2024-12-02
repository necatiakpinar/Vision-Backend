using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Mentor.Command.AddMentorFollower;

public class AddMentorFollowerCommand : IRequest<GenericResponse<AddMentorFollowerResponse>>
{
    public string MentorId { get; set; }
    public string FollowerId { get; set; }
    
}