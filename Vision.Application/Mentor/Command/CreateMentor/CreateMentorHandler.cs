using MediatR;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.Mentor;
using Vision.Domain.Entities;

namespace Vision.Application.Mentor.Command.CreateMentor;

public class CreateMentorHandler : IRequestHandler<CreateMentorCommand, GenericResponse<CreateMentorResponse>>
{ 
    private readonly IMentorReadRepository _mentorReadRepository;
    private readonly IMentorWriteRepository _mentorWriteRepository;

    public CreateMentorHandler(IMentorReadRepository mentorReadRepository, IMentorWriteRepository mentorWriteRepository)
    {
        _mentorReadRepository = mentorReadRepository;
        _mentorWriteRepository = mentorWriteRepository;
    }

    public async Task<GenericResponse<CreateMentorResponse>> Handle(CreateMentorCommand request, CancellationToken cancellationToken)
    {
        var mentor = await _mentorReadRepository.GetMentorByUserId(request.UserId);
        if (mentor != null)
        {
            return new GenericResponse<CreateMentorResponse>()
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "Mentor already exists",
                    Message = "You are already a mentor."
                }
            };
        }
        
        var newMentor = new MentorModel()
        {
            Id = Guid.NewGuid().ToString(),
            MentorId = request.UserId,
            Followers = new List<FollowerModel>()
            
        };

        await _mentorWriteRepository.Table.AddAsync(newMentor, cancellationToken: cancellationToken);
        await _mentorWriteRepository.SaveChangesAsync();
        
        return new GenericResponse<CreateMentorResponse>()
        {
            Data = new CreateMentorResponse()
            {
              Succeeded = true
            }
        };
    }
}