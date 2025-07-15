using GAAPICommon.Messages;
using Guidance.SchedulingClients.Jobs;
using System.Net;

namespace Guidance.SchedulingClients.TaskState.DevConsoleApp;

internal class ClientHandler
{
    static int _counter = 1;
    ITaskStateClient? _client;

    internal void Init()
    {
        _client = ClientFactory.CreateTaskStateClient(IPAddress.Loopback);
        _client.TaskProgressUpdated += Client_TaskProgressUpdated;
    }

    private void Client_TaskProgressUpdated(TaskProgressDto obj)
    {
        Console.WriteLine(_counter.ToString());
        Console.WriteLine("Task ID: " + obj.TaskId.ToString());
        Console.WriteLine("Assigned Agent ID: " + obj.AssignedAgentId.ToString());
        Console.WriteLine("Task Status ID: " + obj.TaskStatus.ToString());
        Console.WriteLine(" ");
        ++_counter;
    }
}
