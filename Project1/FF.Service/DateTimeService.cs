using FF.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Service
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
