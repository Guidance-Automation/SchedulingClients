namespace Guidance.SchedulingClients;

/// <summary>
/// Settings to pass to the given client.
/// </summary>
/// <param name="Subscribe"> If true, spin up a new thread to subscribe and receive updates. </param>
public record ClientSettings(bool Subscribe);
