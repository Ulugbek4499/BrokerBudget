using BrokerBudget.Application.Common.Interfaces;

namespace BrokerBudget.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
