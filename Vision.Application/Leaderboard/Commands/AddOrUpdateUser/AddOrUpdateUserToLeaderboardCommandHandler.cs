using MediatR;
using Microsoft.AspNetCore.Identity;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.Leaderboard;
using Vision.Domain.Identity;

namespace Vision.Application.Leaderboard.Commands.AddOrUpdateUser;

public class AddOrUpdateUserToLeaderboardCommandHandler : IRequestHandler<AddOrUpdateUserToLeaderboardCommand, GenericResponse<AddOrUpdateUserToLeaderboardResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly ILeaderboardReadRepository _leaderboardReadRepository;
    private readonly ILeaderboardWriteRepository _leaderboardWriteRepository;

    public AddOrUpdateUserToLeaderboardCommandHandler(
        UserManager<User> userManager,
        ILeaderboardReadRepository leaderboardReadRepository,
        ILeaderboardWriteRepository leaderboardWriteRepository)
    {
        _userManager = userManager;
        _leaderboardReadRepository = leaderboardReadRepository;
        _leaderboardWriteRepository = leaderboardWriteRepository;
    }

    public async Task<GenericResponse<AddOrUpdateUserToLeaderboardResponse>> Handle(AddOrUpdateUserToLeaderboardCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return new GenericResponse<AddOrUpdateUserToLeaderboardResponse>
            {
                Data = new AddOrUpdateUserToLeaderboardResponse()
                {
                    Succeeded = false
                },
                WarningResult = new WarningResult
                {
                    Title = "User Not Found",
                    Message = "User not found"
                }
            };
        }

        if (request.Score < 0)
        {
            return new GenericResponse<AddOrUpdateUserToLeaderboardResponse>
            {
                Data = new AddOrUpdateUserToLeaderboardResponse()
                {
                    Succeeded = false
                },
                WarningResult = new WarningResult
                {
                    Title = "Invalid Score",
                    Message = "Score cannot be negative"
                }
            };
        }

        await UpdateScoreAsync(request, user, cancellationToken);
        return new GenericResponse<AddOrUpdateUserToLeaderboardResponse>
        {
            Data = new AddOrUpdateUserToLeaderboardResponse()
            {
                Succeeded = true
            },
        };
    }

    public async Task UpdateScoreAsync(AddOrUpdateUserToLeaderboardCommand request, User user, CancellationToken cancellationToken)
    {
        var leaderboardEntry = await _leaderboardReadRepository.GetByUserIdAsync(request.UserId.ToString(), cancellationToken);
        
        if (leaderboardEntry != null)
        {
            leaderboardEntry.Score = request.Score;
            leaderboardEntry.UpdatedAt = DateTime.UtcNow;
            
            _leaderboardWriteRepository.Update(leaderboardEntry);
            await _leaderboardWriteRepository.SaveChangesAsync();
        }
        else
        {
            var newLeaderboardEntry = new Domain.Entities.Leaderboard
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName!,
                UserId = request.UserId,
                Score = request.Score,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _leaderboardWriteRepository.AddAsync(newLeaderboardEntry);
            await _leaderboardWriteRepository.SaveChangesAsync();
        }
    }
}
