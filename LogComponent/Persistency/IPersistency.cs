using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogComponent;


namespace LogComponent
{
    public interface IPersistency
    {
        void Save(LogLine text);
    }
}
