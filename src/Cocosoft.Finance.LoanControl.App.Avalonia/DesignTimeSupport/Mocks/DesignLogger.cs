using Microsoft.Extensions.Logging;
using System;

namespace Cocosoft.Finance.LoanControl.App.Avalonia.DesignTimeSupport.Mocks;

internal class DesignLogger<T> : ILogger<T>
{
    private class DesignLoggerScope : IDisposable
    {
        public void Dispose()
        {
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => new DesignLoggerScope();

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
    }
}
