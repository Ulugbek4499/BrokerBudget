using GameStore.Application.Common.Interfaces;

namespace GameStore.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
