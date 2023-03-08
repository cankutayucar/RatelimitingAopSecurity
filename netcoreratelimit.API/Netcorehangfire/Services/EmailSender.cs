using SendGrid.Helpers.Mail;
using SendGrid;

namespace Netcorehangfire.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Sender(string userId, string message)
        {
            var apiKey = _configuration.GetSection("Apis")["sendgripapi"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cankutayucar@hotmail.com", "cankutay uçar");
            var subject = "site bilgilendirme";
            var to = new EmailAddress("ucarcankutay@gmail.com", "uçar cankutay");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>Site kuralları aşağıdadır</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
