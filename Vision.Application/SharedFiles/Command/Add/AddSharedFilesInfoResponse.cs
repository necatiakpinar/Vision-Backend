using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.SharedFiles.Command.Add;

public class AddSharedFilesInfoResponse : IResponse
{
    public bool Succeeded { get; set; }
}