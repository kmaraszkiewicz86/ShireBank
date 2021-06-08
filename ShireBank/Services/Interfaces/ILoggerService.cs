using System;

namespace ShireBank.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogInformation(string message);

        void LogError(Exception exception);
    }
}