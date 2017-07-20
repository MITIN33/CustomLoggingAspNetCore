using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Identity
{
    public class ColoredConsoleLoggerProvider : ILoggerProvider
    {
        private readonly ColoredConsoleLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ColoredConsoleLogger> _loggers = new ConcurrentDictionary<string, ColoredConsoleLogger>();

        public ColoredConsoleLoggerProvider(ColoredConsoleLoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ColoredConsoleLogger(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }

    public class ColoredConsoleLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
    }


    public class ColoredConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly ColoredConsoleLoggerConfiguration _config;

        public ColoredConsoleLogger(string name, ColoredConsoleLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = _config.Color;
                Console.WriteLine($"{DateTime.UtcNow} - {logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
                Console.ForegroundColor = color;
            }
        }
    }

    public static class ColoredConsoleLoggerEx
    {
        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, ColoredConsoleLoggerConfiguration config)
        {
            loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(config));
            return loggerFactory;
        }
        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory)
        {
            var config = new ColoredConsoleLoggerConfiguration();
            return loggerFactory.AddColoredConsoleLogger(config);
        }
        public static ILoggerFactory AddColoredConsoleLogger(this ILoggerFactory loggerFactory, Action<ColoredConsoleLoggerConfiguration> configure)
        {
            var config = new ColoredConsoleLoggerConfiguration();
            configure(config);
            return loggerFactory.AddColoredConsoleLogger(config);
        }
    }
}
