using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Image.ProfileImage.Queries.Get;

public record GetProfileImageQuery(string ImageId) : IRequest<GenericResponse<GetProfileImageResponse>>;