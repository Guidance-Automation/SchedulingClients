using GAAPICommon.Messages;
using Guidance.SchedulingClients.Jobs;
using System.Net;

namespace Guidance.SchedulingClients.JobState.DevConsoleApp;

internal class ClientHandler
{
    static int _counter = 1;
    IJobStateClient? _client;

    internal void Init()
    {
        _client = ClientFactory.CreateJobStateClient(IPAddress.Loopback);
        _client.JobProgressUpdated += Client_JobProgressUpdated;
    }

    private void Client_JobProgressUpdated(JobProgressDto obj)
    {
        Console.WriteLine(_counter.ToString());

        Console.WriteLine("Job ID: " + obj.JobId.ToString());
        Console.WriteLine("Job Status: " + obj.JobStatus.ToString());
        Console.WriteLine(" ");
        ++_counter;
    }
}
