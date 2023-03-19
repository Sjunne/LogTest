using LogComponent;
using LogComponent.Persistency;

namespace LogUsers.Builder
{
    public class LoggerBuilder : IBuilder
    {
        private IPersistency? _persistance;
   
        public LoggerBuilder LogToFile(string fileDestination)
        {
            if (!Directory.Exists(fileDestination))
            {
                try
                {
                    Directory.CreateDirectory(fileDestination);
                }
                catch { 
                    //handle
                }
            }

           _persistance = new LocalFilePersistency(fileDestination);
            return this;
        }

        public LoggerBuilder BuildLoggerToDB(string connectionString)
        {
            //** Not Implemented **//
            _persistance = new DBPersistency(connectionString);

            return this;
        }

        public ILog Build()
        {
            return new AsyncLog(_persistance);
        }
    }
}
