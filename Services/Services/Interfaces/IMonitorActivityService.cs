using System.Text;
using Models.Enums;

namespace Services.Services.Interfaces
{
    public interface IMonitorActivityService
    {
        StringBuilder ActivityLoggerStringBuilder { get; }

        void AddUserActivity(CustomerActivityType customerActivityType, CustomerActivityPoint customerActivityPoint, object request = null);
    }
}