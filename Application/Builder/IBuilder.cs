using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUsers.Builder
{
    internal interface IBuilder
    {
        LoggerBuilder LogToFile(string fileDestination);
        LoggerBuilder BuildLoggerToDB(string connectionString);
    }
}
