using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Mentor.Command.CreateMentor;

public class CreateMentorResponse : IResponse
{
    public bool Succeeded { get; set; }
}