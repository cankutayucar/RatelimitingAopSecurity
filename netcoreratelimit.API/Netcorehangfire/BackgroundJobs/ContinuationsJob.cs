using System.Diagnostics;

namespace Netcorehangfire.BackgroundJobs
{
    public class ContinuationsJob
    {
        public static void WriteJob(string id, string fileName)
        {
            Hangfire.BackgroundJob.ContinueJobWith(id, () => Debug.WriteLine("metod çalıştı"));
        }
    }
}
