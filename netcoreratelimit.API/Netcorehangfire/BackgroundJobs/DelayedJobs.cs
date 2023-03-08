using Netcorehangfire.Services;

namespace Netcorehangfire.BackgroundJobs
{
    public class DelayedJobs
    {
        public static void ApplyWatermarkJob(string filename, string watermarktext)
        {
            // işlem sırasında belirlenen süreden sonra çalışır
            Hangfire.BackgroundJob.Schedule<IEmailSender>(x => x.Sender(filename, watermarktext), TimeSpan.FromSeconds(30));
        }
    }
}
