using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Contracts.Service
{
    public interface ISecurityService
    {
        string CurrentUser { get; set; }
    }
}
