using MediatR;
using Microsoft.AspNetCore.Identity;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.Mentor;
using Vision.Domain.Entities;
using Vision.Domain.Identity;

namespace Vision.Application.Mentor.Command.AddMentorFollower;

public class AddMentorFollowerHandler : IRequestHandler<AddMentorFollowerCommand, GenericResponse<AddMentorFollowerResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMentorReadRepository _mentorReadRepository;
    private readonly IMentorWriteRepository _mentorWriteRepository;

    public AddMentorFollowerHandler(UserManager<User> userManager, IMentorWriteRepository mentorWriteRepository, IMentorReadRepository mentorReadRepository)
    {
        _userManager = userManager;
        _mentorWriteRepository = mentorWriteRepository;
        _mentorReadRepository = mentorReadRepository;
    }

    public async Task<GenericResponse<AddMentorFollowerResponse>> Handle(AddMentorFollowerCommand command, CancellationToken cancellationToken)
    {
        var mentor = await _mentorReadRepository.GetMentorByUserId(command.MentorId);
        if (mentor == null)
        {
            return new GenericResponse<AddMentorFollowerResponse>()
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "Mentor not found",
                    Message = "Mentor not found."
                }
            };
        }

        var follower = await _userManager.FindByIdAsync(command.FollowerId);
        if (follower == null)
        {
            return new GenericResponse<AddMentorFollowerResponse>()
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "Follower not found",
                    Message = "Follower not found."
                }
            };
        }

        var followerModel = new FollowerModel()
        {
            Id = Guid.NewGuid().ToString(),
            FollowerId = command.FollowerId
        };

        var alreadyFollower = mentor.Followers.Any(f => f.FollowerId == command.FollowerId);
        if (alreadyFollower)
        {
            return new GenericResponse<AddMentorFollowerResponse>()
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "Already follower",
                    Message = "User is already a follower."
                }
            };
        }
        

        mentor.Followers.Add(followerModel);
        await _mentorWriteRepository.SaveChangesAsync();

        return new GenericResponse<AddMentorFollowerResponse>()
        {
            Data = new AddMentorFollowerResponse()
            {
                Succeeded = true
            }
        };
    }
}