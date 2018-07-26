using GAClients;
using SchedulingClients.TaskStateServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingClients.Client_Interfaces
{
    public interface ITaskStateClient : ICallbackClient
    {
        event Action<TaskProgressData> TaskProgressUpdated;
    }
}
