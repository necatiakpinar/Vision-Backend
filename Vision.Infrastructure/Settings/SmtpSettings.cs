namespace Vision.Infrastructure.Settings;

public class SmtpSettings
{
    public const string SectionName = "SmtpSettings";
    public string Name { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
}
