﻿using BaseClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingClients
{
    public interface IJobsStateClient : ICallbackClient
    {
        ServiceOperationResult TryAbortAllJobs(out bool success);

        ServiceOperationResult TryAbortAllJobsForAgent(int agentId, out bool success);

        ServiceOperationResult TryAbortJob(int jobId, out bool success);

        ServiceOperationResult TryAbortTask(int taskId, out bool success);

        ServiceOperationResult TryGetActiveJobIdsForAgent(int agentId, out IEnumerable<int> jobIds);

        ServiceOperationResult TryResolveFailingTask(int taskId, out bool success);

        ServiceOperationResult TryResolveFailingJob(int jobId, out bool success);
    }
}
