using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace TodoApiDTO.Extensions
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;
        private readonly static object _lock = new object();
        public FileLogger(string path) => filePath = path;
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    string path = $"{Directory.GetCurrentDirectory()}\\Logs";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.AppendAllText(path + "\\" + filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }

    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string path;
        public FileLoggerProvider(string _path) => path = _path;
        public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName);

        public void Dispose()
        {
        }
    }


    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
