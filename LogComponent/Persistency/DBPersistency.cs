using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent.Persistency
{
    public class DBPersistency : IPersistency
    {
        private string connectionString;

        public DBPersistency(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Save(LogLine text)
        {
            throw new NotImplementedException();
        }
    }
}
