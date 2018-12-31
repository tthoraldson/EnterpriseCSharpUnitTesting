using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Contracts.Service
{
    public interface ISerialize
    {
        object ReadObject(Stream stream);
    }
}
