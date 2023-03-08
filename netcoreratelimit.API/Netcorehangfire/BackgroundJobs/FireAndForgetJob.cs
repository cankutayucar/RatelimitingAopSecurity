using Netcorehangfire.Services;

namespace Netcorehangfire.BackgroundJobs
{
    public class FireAndForgetJob
    {


        public static void EmailSendToUserJob(string userId, string message)
        {
            // işlem sırasında aynı anda çalışır
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message));
        }




    }
}
