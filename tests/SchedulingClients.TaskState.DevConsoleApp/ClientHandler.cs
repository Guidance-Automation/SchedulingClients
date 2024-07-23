using GAAPICommon.Messages;
using GAClients.SchedulingClients;
using GAClients.SchedulingClients.Jobs;
using System.Net;

namespace SchedulingClients.TaskState.DevConsoleApp;

class ClientHandler
{
    static int _counter = 1;
    ITaskStateClient? _client;

    public void Init()
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
