namespace Guidance.SchedulingClients.JobState.DevConsoleApp;

internal class Program
{
    internal static void Main(string[] _)
    {
        ClientHandler handler = new();
        handler.Init();
    }
}