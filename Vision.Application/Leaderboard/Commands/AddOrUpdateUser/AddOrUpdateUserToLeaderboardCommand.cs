using MediatR;
using MongoDB.Bson;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Leaderboard.Commands.AddOrUpdateUser;

public record AddOrUpdateUserToLeaderboardCommand(string UserId, int Score) : IRequest<GenericResponse<AddOrUpdateUserToLeaderboardResponse>>;