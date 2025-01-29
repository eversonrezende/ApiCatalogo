using System.Collections.Concurrent;

namespace APICatalogo.Logging;

public class CustomLoggerProvider : ILoggerProvider
{
    readonly CustomLoggerProviderConfiguration _loggerConfig;

    readonly ConcurrentDictionary<string, CustomerLogger> loggers =
        new ConcurrentDictionary<string, CustomerLogger>();

    public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
    {
        _loggerConfig = config;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, _loggerConfig));
    }

    public void Dispose()
    {
        loggers.Clear();
    }
}
