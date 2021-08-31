using BaseClients.Core;
using GAAPICommon.Architecture;
using SchedulingClients.Core.JobBuilderServiceReference;
using System;
using System.Net;

namespace SchedulingClients.Core
{
    internal class JobBuilderClient : AbstractClient<IJobBuilderService>, IJobBuilderClient
    {
        private bool isDisposed = false;

        public JobBuilderClient(Uri netTcpUri)
            : base(netTcpUri)
        {
        }

        public IServiceCallResult CommitJob(int jobId, int agentId = -1)
        {
            Logger.Trace($"CommitJob() jobId:{jobId} agentId:{agentId}");
            return HandleAPICall(e => e.CommitJob(jobId, agentId));
        }

        public IServiceCallResult<JobDto> CreateJob(JobPriority jobPriority = JobPriority.Normal)
        {
            Logger.Trace("CreateJob()");
            return HandleAPICall<JobDto>(e => e.CreateJob(jobPriority));
        }

        public IServiceCallResult<int> CreateOrderedListTask(int parentListTaskId)
        {
            Logger.Trace($"CreateOrderedListTask() parentListTaskId:{parentListTaskId}");
            return HandleAPICall<int>(e => e.CreateOrderedListTask(parentListTaskId));
        }

        public IServiceCallResult<int> CreateUnorderedListTask(int parentListTaskId)
        {
            Logger.Trace($"CreateUnorderedListTask() parentListTaskId:{parentListTaskId}");
            return HandleAPICall<int>(e => e.CreateUnorderedListTask(parentListTaskId));
        }

        public IServiceCallResult<int> CreateAtomicMoveListTask(int parentListTaskId)
        {
            Logger.Trace($"CreateAtomicMoveListTask() parentListTaskId:{parentListTaskId}");
            return HandleAPICall<int>(e => e.CreateAtomicMoveListTask(parentListTaskId));
        }

        public IServiceCallResult<int> CreateServicingTask(int parentListTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration = default)
        {
            Logger.Trace($"CreateServicingTask() parentListTaskId:{parentListTaskId} nodeId:{nodeId} serviceType:{serviceType} expectedDuration:{expectedDuration}");
            return HandleAPICall<int>(e => e.CreateServicingTask(parentListTaskId, nodeId, serviceType, expectedDuration));
        }

        public IServiceCallResult<int> CreateSleepingTask(int parentListTaskId, int nodeId, TimeSpan expectedDuration = default)
        {
            Logger.Trace($"CreateSleepingTask() parentListTaskId:{parentListTaskId} nodeId:{nodeId} expectedDuration:{expectedDuration}");
            return HandleAPICall<int>(e => e.CreateSleepingTask(parentListTaskId, nodeId, expectedDuration));
        }

        public IServiceCallResult<int> CreateAwaitingTask(int parentListTaskId, int nodeId)
        {
            Logger.Trace($"CreateAwaitingTask() parentListTaskId:{parentListTaskId} nodeId:{nodeId})");
            return HandleAPICall<int>(e => e.CreateAwaitingTask(parentListTaskId, nodeId));
        }

        public IServiceCallResult<int> CreateGoToNodeTask(int parentListTaskId, int nodeId)
        {
            Logger.Trace($"CreateGoToNodeTask() parentListTaskId:{parentListTaskId} nodeId:{nodeId}");
            return HandleAPICall<int>(e => e.CreateGoToNodeTask(parentListTaskId, nodeId));
        }

        public IServiceCallResult<int> CreateAtomicMoveTask(int parentAtomicMoveListTaskId, int moveId)
        {
            Logger.Trace($"CreateAtomicMoveTask() parentAtomicMoveListTaskId:{parentAtomicMoveListTaskId} moveId:{moveId}");
            return HandleAPICall<int>(e => e.CreateAtomicMoveTask(parentAtomicMoveListTaskId, moveId));
        }

        public IServiceCallResult IssueEnumDirective(int taskId, string parameterAlias, byte value)
        {
            if (string.IsNullOrEmpty(parameterAlias))
                throw new ArgumentOutOfRangeException("parameterAlias");

            Logger.Trace($"IssueEnumDirective() taskId:{taskId} parameterAlias:{parameterAlias} value:{value}");
            return HandleAPICall(e => e.IssueEnumDirective(taskId, parameterAlias, value));
        }

        public IServiceCallResult IssueShortDirective(int taskId, string parameterAlias, short value)
        {
            if (string.IsNullOrEmpty(parameterAlias))
                throw new ArgumentOutOfRangeException("parameterAlias");

            Logger.Trace($"IssueShortDirective() taskId:{taskId} parameterAlias:{parameterAlias} value:{value}");
            return HandleAPICall(e => e.IssueShortDirective(taskId, parameterAlias, value));
        }

        public IServiceCallResult IssueUShortDirective(int taskId, string parameterAlias, ushort value)
        {
            if (string.IsNullOrEmpty(parameterAlias))
                throw new ArgumentOutOfRangeException("parameterAlias");

            Logger.Trace($"IssueUShortDirective() taskId:{taskId} parameterAlias:{parameterAlias} value:{value}");
            return HandleAPICall(e => e.IssueUShortDirective(taskId, parameterAlias, value));
        }

        public IServiceCallResult IssueFloatDirective(int taskId, string parameterAlias, float value)
        {
            if (string.IsNullOrEmpty(parameterAlias))
                throw new ArgumentOutOfRangeException("parameterAlias");

            Logger.Trace($"IssueFloatDirective() taskId:{taskId} parameterAlias:{parameterAlias} value:{value}");
            return HandleAPICall(e => e.IssueFloatDirective(taskId, parameterAlias, value));
        }

        public IServiceCallResult IssueIPAddressDirective(int taskId, string parameterAlias, IPAddress ipAddress)
        {
            if (string.IsNullOrEmpty(parameterAlias))
                throw new ArgumentOutOfRangeException("parameterAlias");

            if (ipAddress == null)
                throw new ArgumentNullException("ipAddress");

            Logger.Trace($"IssueIPAddressDirective() taskId:{taskId} parameterAlias:{parameterAlias} ipAddress:{ipAddress}");
            return HandleAPICall(e => e.IssueIPAddressDirective(taskId, parameterAlias, ipAddress));
        }

        public IServiceCallResult<bool> BeginEditingJob(int jobId)
        {
            Logger.Trace($"BeginEditingJob() jobId:{jobId}");
            return HandleAPICall<bool>(e => e.BeginEditingJob(jobId));
        }

        public IServiceCallResult FinishEditingJob(int jobId)
        {
            Logger.Trace($"FinishEditingJob() jobId:{jobId}");
            return HandleAPICall(e => e.FinishEditingJob(jobId));
        }
    }
}