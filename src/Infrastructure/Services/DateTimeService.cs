using PoshtaBy.Application.Common.Interfaces;
using System;

namespace PoshtaBy.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
