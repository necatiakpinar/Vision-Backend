using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Mentor.Command.DeleteMentor;

public class DeleteMentorResponse : IResponse
{
    public bool Succeeded { get; set; }
}