namespace Netcorehangfire.Services
{
    public interface IEmailSender
    {
        Task Sender(string userId,string message);
    }
}
