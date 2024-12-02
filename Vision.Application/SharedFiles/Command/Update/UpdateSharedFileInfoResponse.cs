using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.SharedFiles.Command.Update;

public class UpdateSharedFileInfoResponse : IResponse
{
    public bool Succeeded { get; set; }
}