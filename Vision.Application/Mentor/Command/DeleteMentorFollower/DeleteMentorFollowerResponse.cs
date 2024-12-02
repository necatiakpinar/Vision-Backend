using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Mentor.Command.DeleteMentorFollower;

public class DeleteMentorFollowerResponse : IResponse
{
    public bool Succeeded { get; set; }
}