using SchedulingClients.JobBuilderServiceReference;
using System;
using System.Linq;
using SchedulingClients.MapServiceReference;
using System.Collections.Generic;
using System.Windows;
using GAClients;

namespace SchedulingClients.UserControls
{
    public static class JobBuilderClient_ExtensionMethods
    {
        private static int HandleCreatedUnorderedListTask(IJobBuilderClient jobBuilder, int parentListTaskId)
        {
            int unorderedListTaskId;

            ServiceOperationResult result = jobBuilder.TryCreateUnorderedListTask(parentListTaskId, out unorderedListTaskId);

            if (!result.IsSuccessfull)
            {
                MessageBox.Show("Failed to create unordered list task");
            }

            return unorderedListTaskId;
        }

        public static void MultiPickJobTest(this IJobBuilderClient jobBuilder, IEnumerable<NodeData> nodes)
        {
            JobData jobData;

            if (jobBuilder.TryCreateJob(out jobData) == true)
            {
                int unorderedListTaskId = HandleCreatedUnorderedListTask(jobBuilder, jobData.RootOrderedListTaskId);

                if (unorderedListTaskId >= 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        NodeData pickNode = GetRandomNode(nodes, MapServiceReference.ServiceType.ManualLoadHandling);

                        if (pickNode != null)
                        {
                            int nodeTaskId;
                            jobBuilder.TryCreateServicingTask(unorderedListTaskId, pickNode.MapItemId, JobBuilderServiceReference.ServiceType.ManualLoadHandling, out nodeTaskId, TimeSpan.FromSeconds(30));
                        }
                    }

                    NodeData dropNode = GetRandomNode(nodes, MapServiceReference.ServiceType.ManualLoadHandling);

                    if (dropNode != null)
                    {
                        int dropNodeId;
                        jobBuilder.TryCreateServicingTask(jobData.RootOrderedListTaskId, dropNode.MapItemId, JobBuilderServiceReference.ServiceType.ManualLoadHandling, out dropNodeId, TimeSpan.FromSeconds(10));
                    }

                    NodeData parkNode = GetRandomNode(nodes, MapServiceReference.ServiceType.Park);

                    if (parkNode != null)
                    {
                        int parkNodeId;
                        jobBuilder.TryCreateServicingTask(jobData.RootOrderedListTaskId, parkNode.MapItemId, JobBuilderServiceReference.ServiceType.Park, out parkNodeId, TimeSpan.FromSeconds(10));
                    }
                }

                bool success;
                if (jobBuilder.TryCommit(jobData.JobId, out success) != true)
                {
                    MessageBox.Show(string.Format("Failed to commit job with jobId: {0}", jobData.JobId));
                }
            }
            else
            {
                MessageBox.Show("Failed to create job.");
            }
        }

        public static void MoveJobTest(this IJobBuilderClient jobBuilder, IEnumerable<NodeData> nodes, bool isFinalised)
        {
            JobData jobData;

            if (jobBuilder.TryCreateJob(out jobData) == true)
            {
                int piplineId;
                jobBuilder.TryCreatePipelinedTask(jobData.RootOrderedListTaskId, isFinalised, out piplineId);

                NodeData pickNode = GetRandomNode(nodes, MapServiceReference.ServiceType.ManualLoadHandling);
                NodeData pickNode2 = GetRandomNode(nodes, MapServiceReference.ServiceType.Charge);

                int pickNodeId, pickNodeId2;
                jobBuilder.TryCreateMovingTask(piplineId, pickNode.MapItemId, out pickNodeId);
                jobBuilder.TryCreateMovingTask(piplineId, pickNode2.MapItemId, out pickNodeId2);

                bool success;
                jobBuilder.TryCommit(jobData.JobId, out success);
            }
        }

        private static NodeData GetRandomNode(IEnumerable<NodeData> nodes, MapServiceReference.ServiceType serviceType)
        {
            List<NodeData> subSet = nodes.Where(e => e.Services.Contains(serviceType)).ToList();

            if (subSet.Any())
            {
                Random random = new Random();
                return subSet[random.Next(0, subSet.Count)];
            }

            return null;
        }
    }
}