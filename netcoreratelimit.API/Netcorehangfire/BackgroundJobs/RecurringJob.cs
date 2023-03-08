using Hangfire;
using System.Diagnostics;

namespace Netcorehangfire.BackgroundJobs
{
    public class RecurringJob
    {
        public static void RepotingJob()
        {
            // belirli periyotlarda çalışacak joblar bunlardır
            Hangfire.RecurringJob.AddOrUpdate("reportjob1", () => EmailReport(), Cron.Minutely);
        }
        public static void EmailReport()
        {
            Debug.WriteLine("Rapor, email olarak gönderildi");
        }
    }
}
