using System;
using SchedulingClients.JobStateServiceReference;

namespace SchedulingClients
{
    public class JobStateServiceCallback : IJobStateServiceCallback
    {
        public JobStateServiceCallback()
        {
        }

        public event Action<JobProgressData> JobProgressUpdated;

        public void OnCallback(JobProgressData jobProgressData)
        {
            Action<JobProgressData> handlers = JobProgressUpdated;

            if (handlers != null)
            {
                foreach (Action<JobProgressData> handler in handlers.GetInvocationList())
                {
                    handler.BeginInvoke(jobProgressData, null, null);
                }
            }
        }
    }
}