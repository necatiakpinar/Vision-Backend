using MediatR;
using Microsoft.AspNetCore.Identity;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.Mentor;
using Vision.Domain.Identity;

namespace Vision.Application.Mentor.Command.DeleteMentorFollower;

public class DeleteMentorFollowerHandler : IRequestHandler<DeleteMentorFollowerCommand, GenericResponse<DeleteMentorFollowerResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMentorReadRepository _mentorReadRepository;
    private readonly IMentorWriteRepository _mentorWriteRepository;

    public DeleteMentorFollowerHandler(UserManager<User> userManager, IMentorWriteRepository mentorWriteRepository, IMentorReadRepository mentorReadRepository)
    {
        _userManager = userManager;
        _mentorWriteRepository = mentorWriteRepository;
        _mentorReadRepository = mentorReadRepository;
    }

    public async Task<GenericResponse<DeleteMentorFollowerResponse>> Handle(DeleteMentorFollowerCommand command, CancellationToken cancellationToken)
    {
        var mentor = await _mentorReadRepository.GetMentorByUserId(command.MentorId);
        if (mentor == null)
        {
            return new GenericResponse<DeleteMentorFollowerResponse>()
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
            return new GenericResponse<DeleteMentorFollowerResponse>()
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "Follower not found",
                    Message = "Follower not found."
                }
            };
        }
        
        var alreadyFollower = mentor.Followers.FirstOrDefault(f => f.FollowerId == command.FollowerId);
        if (alreadyFollower != null)
        {
            mentor.Followers.Remove(alreadyFollower);
            await _mentorWriteRepository.SaveChangesAsync();

            return new GenericResponse<DeleteMentorFollowerResponse>()
            {
                Data = new DeleteMentorFollowerResponse()
                {
                    Succeeded = true
                }
            };
            
        }
        
        return new GenericResponse<DeleteMentorFollowerResponse>()
        {
            Data = null,
            WarningResult = new()
            {
                Title = "Not a follower",
                Message = "User is not a follower."
            }
        };
        
    }
}