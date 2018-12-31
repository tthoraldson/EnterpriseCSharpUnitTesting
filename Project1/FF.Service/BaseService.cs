using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Service
{
    public class BaseService
    {
        protected Logger Log { get; private set; }

        protected BaseService()
        {
            Log = LogManager.GetLogger(GetType().FullName);
        }

    }
}
