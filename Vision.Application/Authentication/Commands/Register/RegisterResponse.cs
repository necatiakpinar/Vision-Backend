using Vision.Application.Authentication.Common.Interfaces;
using Vision.Application.Common;

namespace Vision.Application.Authentication.Commands.Register;

public class RegisterResponse : IResponse  
{
    public bool Succeeded { get; set; }
}