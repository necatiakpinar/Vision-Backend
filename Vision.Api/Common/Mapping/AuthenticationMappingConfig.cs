using Mapster;
using Vision.Application.Authentication.Commands.Register;
using Vision.Application.Image.ProfileImage.Queries.Get;
using Vision.Application.Leaderboard.Queries;
using Vision.Contracts.Authentication;
using Vision.Contracts.Images.ProfileImage;
using Vision.Contracts.Leaderboard;

namespace Vision.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        // config for leaderboard
        // config.NewConfig<AddOrUpdateUserToLeaderboardCommand, AddOrUpdateUserToLeaderboardResponse>()

        // config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        //     .Map(dest => dest, src => src.User);

        // config.NewConfig<GetLeaderboardUsersRequest, GetLeaderboardUsersQuery>()
        //     .Map(dest => dest.PageNumber, src => src.PageNumber);
        //.Map(dest => dest.PageSize, src => -1);


    }
}