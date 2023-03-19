namespace LogUsers.Builder
{
    public interface IBuilder
    {
        LoggerBuilder LogToFile(string fileDestination);
        LoggerBuilder BuildLoggerToDB(string connectionString);
    }
}
