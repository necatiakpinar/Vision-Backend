using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.Authentication.Commands.Delete;
using Vision.Application.Authentication.Commands.Register;
using Vision.Application.Authentication.Common;
using Vision.Application.Authentication.Queries.GetUsers;
using Vision.Application.Authentication.Queries.Login;
using Vision.Application.Services;
using Vision.Domain.Identity;
using ForgotPasswordRequest = Vision.Contracts.Authentication.ForgotPasswordRequest;
using LoginRequest = Vision.Contracts.Authentication.LoginRequest;
using RegisterRequest = Vision.Contracts.Authentication.RegisterRequest;
using ResetPasswordRequest = Vision.Contracts.Authentication.ResetPasswordRequest;

namespace Vision.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IRedisService _redisService;

    public AuthenticationController(ISender mediator, IMapper mapper, UserManager<User> userManager, IEmailService emailService, IRedisService redisService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
        _redisService = redisService;
    }
    
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        GenericResponse<RegisterResponse> registerResponse = await _mediator.Send(command);

        return Ok(_mapper.Map<GenericResponse<RegisterResponse>>(registerResponse));
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult<GenericResponse<LoginResponse>>> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        GenericResponse<LoginResponse> loginResponse = await _mediator.Send(query);
        _redisService.SetValue("LastLoginDate", DateTime.UtcNow.ToString());
        
        Console.WriteLine(_redisService.GetValue("LastLoginDate"));
        return Ok(_mapper.Map<GenericResponse<LoginResponse>>(loginResponse));
    }

    [Authorize(Roles = "MEMBER")]
    [HttpPost("[action]")]
    public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery query)
    {
        GenericResponse<GetUsersResponse> response = await _mediator.Send(query);
        return Ok(response);
    }

    [Authorize(Roles = "MEMBER")]
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand command)
    {
        GenericResponse<DeleteUserResponse> response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Ok(new { Message = "If the email is correct, you will receive a password reset email." });
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = Url.Action("ResetPassword", "Authentication", new { email = request.Email, token }, Request.Scheme);
        
        _emailService.SendEmail(request.Email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");

        return Ok(new { Message = "If the email is correct, you will receive a password reset email." });
    }

    [HttpGet("[action]")]
    [AllowAnonymous]
    public IActionResult ResetPassword(string email, string token)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest("Invalid password reset token.");
        }
        
        return RedirectToPage("/Authentication/ResetPassword", new { email, token });
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid Request.");
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return BadRequest("Invalid Request.");
        }

        var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!resetPassResult.Succeeded)
        {
            foreach (var error in resetPassResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        return Ok(new { Message = "Password has been reset successfully." });
    }



}