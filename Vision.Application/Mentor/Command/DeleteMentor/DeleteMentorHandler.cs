using MediatR;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.Mentor;

namespace Vision.Application.Mentor.Command.DeleteMentor;

public class DeleteMentorHandler : IRequestHandler<DeleteMentorCommand, GenericResponse<DeleteMentorResponse>>
{ 
    private readonly IMentorReadRepository _mentorReadRepository;
    private readonly IMentorWriteRepository _mentorWriteRepository;

    public DeleteMentorHandler(IMentorReadRepository mentorReadRepository, IMentorWriteRepository mentorWriteRepository)
    {
        _mentorReadRepository = mentorReadRepository;
        _mentorWriteRepository = mentorWriteRepository;
    }

    public async Task<GenericResponse<DeleteMentorResponse>> Handle(DeleteMentorCommand command, CancellationToken cancellationToken)
    {
        var mentor = await _mentorReadRepository.GetMentorByUserId(command.MentorId);
        if (mentor == null)
        {
            return new GenericResponse<DeleteMentorResponse>()
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "Mentor not found",
                    Message = "Mentor not found."
                }
            };
        }
        
        _mentorWriteRepository.Table.Remove(mentor);
        await _mentorWriteRepository.SaveChangesAsync();
        
        return new GenericResponse<DeleteMentorResponse>()
        {
            Data = new DeleteMentorResponse()
            {
              Succeeded = true
            }
        };
    }

    
}