namespace AdvicerApp.BL.ExternalServices.Interfaces;

public interface IEmailSend
{
    Task SendEmailAsync(string reciever, string name, string code);

}
