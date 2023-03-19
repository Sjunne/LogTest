# Log	Task
**Read Task description first**

## Architectural changes

A Builder pattern was implemented for easy access to the logger for clients/users and providing choice of log-destinations.
The pattern provides easy configurations for logger by applying "method dotting", If new settings are later provided (E.g,. security for database), 
they are easily through a new method in IBuilder-interface.
```
var logger = new LoggerBuilder()
                .LogToFile(@"C:\LogTest")
                .Build();
```

Currently two log-destinations are provided as a example. BuildLoggerToDB is just for showcasing, will ultimately throw "NotImplemented".
```
LoggerBuilder LogToFile(string fileDestination);
LoggerBuilder BuildLoggerToDB(string connectionString);
```

        
