using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Mentor.Command.AddMentorFollower;

public class AddMentorFollowerResponse : IResponse
{
    public bool Succeeded { get; set; }
}