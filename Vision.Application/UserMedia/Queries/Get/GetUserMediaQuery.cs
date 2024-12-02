using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.UserMedia.Queries.Get;

public record GetUserMediaQuery(string FileId) : IRequest<GenericResponse<GetUserMediaResponse>>;