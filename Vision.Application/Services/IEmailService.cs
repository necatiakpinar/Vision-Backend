namespace Vision.Application.Services;

public interface IEmailService
{
    public void SendEmail(string toEmail, string subject, string body);
}