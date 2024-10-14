using System;
using BaseClients;
using agent = SchedulingClients.AgentServiceReference;
using jobBuilder = SchedulingClients.JobBuilderServiceReference;
using jobs = SchedulingClients.JobsStateServiceReference;
using maps = SchedulingClients.MapServiceReference;
using job = SchedulingClients.JobStateServiceReference;
using servicing = SchedulingClients.ServicingServiceReference;

namespace SchedulingClients
{
    internal static class ServiceOperationResultFactory
    {
        public static ServiceOperationResult FromServicingServiceCallData(servicing.ServiceCallData serviceCallData)
        {
            Exception serviceException = string.IsNullOrEmpty(serviceCallData.Message) ? null : new Exception(serviceCallData.Message);

            return new ServiceOperationResult
                (
                    (uint)serviceCallData.ServiceCode,
                    serviceCallData.ServiceCode.ToString(),
                    serviceException,
                    null
                );
        }

        public static ServiceOperationResult FromMapServiceCallData(maps.ServiceCallData serviceCallData)
        {
            Exception serviceException = string.IsNullOrEmpty(serviceCallData.Message) ? null : new Exception(serviceCallData.Message);

            return new ServiceOperationResult
                (
                    (uint)serviceCallData.ServiceCode,
                    serviceCallData.ServiceCode.ToString(),
                    serviceException,
                    null
                );
        }

        public static ServiceOperationResult FromJobStateServiceCallData(job.ServiceCallData serviceCallData)
        {
            Exception serviceException = string.IsNullOrEmpty(serviceCallData.Message) ? null : new Exception(serviceCallData.Message);

            return new ServiceOperationResult
                (
                    (uint)serviceCallData.ServiceCode,
                    serviceCallData.ServiceCode.ToString(),
                    serviceException,
                    null
                );
        }

        public static ServiceOperationResult FromJobsStateServiceCallData(jobs.ServiceCallData serviceCallData)
        {
            Exception serviceException = string.IsNullOrEmpty(serviceCallData.Message) ? null : new Exception(serviceCallData.Message);

            return new ServiceOperationResult
                (
                    (uint)serviceCallData.ServiceCode,
                    serviceCallData.ServiceCode.ToString(),
                    serviceException,
                    null
                );
        }

        public static ServiceOperationResult FromJobBuilderServiceCallData(jobBuilder.ServiceCallData serviceCallData)
        {
            Exception serviceException = string.IsNullOrEmpty(serviceCallData.Message) ? null : new Exception(serviceCallData.Message);

            return new ServiceOperationResult
                (
                    (uint)serviceCallData.ServiceCode,
                    serviceCallData.ServiceCode.ToString(),
                    serviceException,
                    null
                );
        }

        public static ServiceOperationResult FromAgentServiceCallData(agent.ServiceCallData serviceCallData)
        {
            Exception serviceException = string.IsNullOrEmpty(serviceCallData.Message) ? null : new Exception(serviceCallData.Message);

            return new ServiceOperationResult
                (
                    (uint)serviceCallData.ServiceCode,
                    serviceCallData.ServiceCode.ToString(),
                    serviceException,
                    null
                );
        }
    }
}
