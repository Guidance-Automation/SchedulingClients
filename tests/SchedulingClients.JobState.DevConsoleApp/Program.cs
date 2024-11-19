namespace SchedulingClients.JobState.DevConsoleApp;

class Program
{
    static void Main(string[] _)
    {
        ClientHandler handler = new();
        handler.Init();
    }
}