using SchedulingClients.TaskStateServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingClients
{
    public class TaskStateServiceCallback : ITaskStateServiceCallback
    {
        public TaskStateServiceCallback()
        {
        }

        public event Action<TaskProgressData> TaskProgressUpdated;

        public void OnCallback(TaskProgressData taskProgressData)
        {
            Action<TaskProgressData> handlers = TaskProgressUpdated;

            if (handlers != null)
            {
                foreach (Action<TaskProgressData> handler in handlers.GetInvocationList())
                {
                    handler.BeginInvoke(taskProgressData, null, null);
                }
            }
        }
    }
}
