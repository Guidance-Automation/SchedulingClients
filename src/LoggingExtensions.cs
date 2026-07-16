using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Guidance.SchedulingClients;

internal static class LoggingExtensions
{
    private static readonly LogDefineOptions _skipEnabledCheck = new() { SkipEnabledCheck = true };
    private static readonly ConcurrentDictionary<(LogLevel Level, string Message), Action<ILogger, Exception?>> _messages = new();

    public static void LogTraceIfEnabled(this ILogger logger, string message)
        => LogIfEnabled(logger, LogLevel.Trace, null, message);

    public static void LogTraceIfEnabled<T0>(this ILogger logger, string message, T0 argument0)
        => LogIfEnabled(logger, LogLevel.Trace, null, message, argument0);

    public static void LogTraceIfEnabled<T0, T1>(this ILogger logger, string message, T0 argument0, T1 argument1)
        => LogIfEnabled(logger, LogLevel.Trace, null, message, argument0, argument1);

    public static void LogTraceIfEnabled<T0, T1, T2>(this ILogger logger, string message, T0 argument0, T1 argument1, T2 argument2)
        => LogIfEnabled(logger, LogLevel.Trace, null, message, argument0, argument1, argument2);

    public static void LogTraceIfEnabled<T0, T1, T2, T3>(this ILogger logger, string message, T0 argument0, T1 argument1, T2 argument2, T3 argument3)
        => LogIfEnabled(logger, LogLevel.Trace, null, message, argument0, argument1, argument2, argument3);

    public static void LogDebugIfEnabled(this ILogger logger, string message)
        => LogIfEnabled(logger, LogLevel.Debug, null, message);

    public static void LogInformationIfEnabled(this ILogger logger, string message)
        => LogIfEnabled(logger, LogLevel.Information, null, message);

    public static void LogWarningIfEnabled(this ILogger logger, Exception? exception, string message)
        => LogIfEnabled(logger, LogLevel.Warning, exception, message);

    public static void LogErrorIfEnabled<T0, T1>(this ILogger logger, string message, T0 argument0, T1 argument1)
        => LogIfEnabled(logger, LogLevel.Error, null, message, argument0, argument1);

    public static void LogErrorIfEnabled(this ILogger logger, Exception? exception, string message)
        => LogIfEnabled(logger, LogLevel.Error, exception, message);

    public static void LogErrorIfEnabled<T0>(this ILogger logger, Exception? exception, string message, T0 argument0)
        => LogIfEnabled(logger, LogLevel.Error, exception, message, argument0);

    private static void LogIfEnabled(ILogger logger, LogLevel level, Exception? exception, string message)
    {
        if (!logger.IsEnabled(level))
        {
            return;
        }

        Action<ILogger, Exception?> log = _messages.GetOrAdd(
            (level, message),
            static key => LoggerMessage.Define(key.Level, default, key.Message, _skipEnabledCheck));
        log(logger, exception);
    }

    private static void LogIfEnabled<T0>(ILogger logger, LogLevel level, Exception? exception, string message, T0 argument0)
    {
        if (!logger.IsEnabled(level))
        {
            return;
        }

        Action<ILogger, T0, Exception?> log = MessageCache<T0>._messages.GetOrAdd(
            (level, message),
            static key => LoggerMessage.Define<T0>(key.Level, default, key.Message, _skipEnabledCheck));
        log(logger, argument0, exception);
    }

    private static void LogIfEnabled<T0, T1>(ILogger logger, LogLevel level, Exception? exception, string message, T0 argument0, T1 argument1)
    {
        if (!logger.IsEnabled(level))
        {
            return;
        }

        Action<ILogger, T0, T1, Exception?> log = MessageCache<T0, T1>._messages.GetOrAdd(
            (level, message),
            static key => LoggerMessage.Define<T0, T1>(key.Level, default, key.Message, _skipEnabledCheck));
        log(logger, argument0, argument1, exception);
    }

    private static void LogIfEnabled<T0, T1, T2>(ILogger logger, LogLevel level, Exception? exception, string message, T0 argument0, T1 argument1, T2 argument2)
    {
        if (!logger.IsEnabled(level))
        {
            return;
        }

        Action<ILogger, T0, T1, T2, Exception?> log = MessageCache<T0, T1, T2>._messages.GetOrAdd(
            (level, message),
            static key => LoggerMessage.Define<T0, T1, T2>(key.Level, default, key.Message, _skipEnabledCheck));
        log(logger, argument0, argument1, argument2, exception);
    }

    private static void LogIfEnabled<T0, T1, T2, T3>(ILogger logger, LogLevel level, Exception? exception, string message, T0 argument0, T1 argument1, T2 argument2, T3 argument3)
    {
        if (!logger.IsEnabled(level))
        {
            return;
        }

        Action<ILogger, T0, T1, T2, T3, Exception?> log = MessageCache<T0, T1, T2, T3>._messages.GetOrAdd(
            (level, message),
            static key => LoggerMessage.Define<T0, T1, T2, T3>(key.Level, default, key.Message, _skipEnabledCheck));
        log(logger, argument0, argument1, argument2, argument3, exception);
    }

    private static class MessageCache<T0>
    {
        internal static readonly ConcurrentDictionary<(LogLevel Level, string Message), Action<ILogger, T0, Exception?>> _messages = new();
    }

    private static class MessageCache<T0, T1>
    {
        internal static readonly ConcurrentDictionary<(LogLevel Level, string Message), Action<ILogger, T0, T1, Exception?>> _messages = new();
    }

    private static class MessageCache<T0, T1, T2>
    {
        internal static readonly ConcurrentDictionary<(LogLevel Level, string Message), Action<ILogger, T0, T1, T2, Exception?>> _messages = new();
    }

    private static class MessageCache<T0, T1, T2, T3>
    {
        internal static readonly ConcurrentDictionary<(LogLevel Level, string Message), Action<ILogger, T0, T1, T2, T3, Exception?>> _messages = new();
    }
}
