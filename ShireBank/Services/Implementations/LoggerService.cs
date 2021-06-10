using System;
using Microsoft.Extensions.Logging;
using ShireBank.Services.Interfaces;

namespace ShireBank.Services.Implementations
{
    public sealed class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(20, message);
        }

        public void LogError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}