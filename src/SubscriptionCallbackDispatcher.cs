using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients;

internal static class SubscriptionCallbackDispatcher
{
    public static void Invoke<T>(
        Action<T>? callbacks,
        T update,
        ILogger? logger,
        string clientName)
    {
        if (callbacks == null)
            return;

        foreach (Action<T> callback in callbacks.GetInvocationList().Cast<Action<T>>())
        {
            try
            {
                callback(update);
            }
            catch (Exception ex)
            {
                logger?.LogWarningIfEnabled(
                    ex,
                    $"[{clientName}] Subscription callback failed");
            }
        }
    }

    public static async Task InvokeAsync<T>(
        Func<T, Task>? callbacks,
        T update,
        ILogger? logger,
        string clientName)
    {
        if (callbacks == null)
            return;

        foreach (Func<T, Task> callback in callbacks.GetInvocationList().Cast<Func<T, Task>>())
        {
            try
            {
                await callback(update);
            }
            catch (Exception ex)
            {
                logger?.LogWarningIfEnabled(
                    ex,
                    $"[{clientName}] Subscription callback failed");
            }
        }
    }
}
