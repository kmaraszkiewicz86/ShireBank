using System.Text;
using System.Threading;
using Models.Enums;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class MonitorActivityService : IMonitorActivityService
    {
        public StringBuilder ActivityLoggerStringBuilder { get; init; }

        public MonitorActivityService()
        {
            ActivityLoggerStringBuilder = new StringBuilder();
        }

        public void AddUserActivity(CustomerActivityType customerActivityType, CustomerActivityPoint customerActivityPoint, object request = null)
        {
            var requestData = request != null ? $"{{ Request data: {request}}}" : null;
            var activityMessage = $"[{Thread.CurrentThread.ManagedThreadId}]: {customerActivityType}[{customerActivityPoint}]{requestData}";

            ActivityLoggerStringBuilder.AppendLine(activityMessage);
        }
    }
}
