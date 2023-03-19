# Log	Task
**Read Task description first**

## Architectural changes

A Builder pattern was implemented for easy access to the logger for clients/users and providing choice of log-destinations.
The pattern provides easy configurations for logger by applying "method dotting", If new settings are later provided (E.g,. security for database), 
they are easily through a new method in IBuilder-interface.
```ruby
var logger = new LoggerBuilder()
                .LogToFile(@"C:\LogTest")
                .Build();
```

Currently two log-destinations are provided as a example. BuildLoggerToDB is just for showcasing, will ultimately throw "NotImplemented".
```ruby
LoggerBuilder LogToFile(string fileDestination);
LoggerBuilder BuildLoggerToDB(string connectionString);
```

To let AsyncLog (logger class) handle the differentiating. A Persistency Interface have been implemented. (IPersistency). 
A IPersistency class is simply required to implement a "Save(string text)" method. Which will persist the data in the given log-location.
Again "DBPersistency" is not implemented, but constructed for show-casing. 
"LocalFilePersistency" will handle writing to a File while satisfying demands in case. 

## Design changes
The list of LogLines was changed into a easiere queue class (BlockingCollection). 
A BlockingCollectiong provides easy access to using CancellationTokens, which in turn implements the required demand#3

3: It must be possible to stop the component in two ways:
* Ask it to stop right away and if any outstanding logs they are not written
* Ask it to stop by wait for it to finish writing outstanding logs if any

```ruby
        public void StopWithoutFlush()
        {
            _logQueue.CompleteAdding();
            _tokenSource.Cancel();
            _exit = true;
        }

        public void StopWithFlush()
        {
            _logQueue.CompleteAdding();
            _exit = true;
        }
```
By implementing with a tokensource, it can stop withoutflush simply by calling the cancel method. Which is then checked in every iteration of the foreach loop.
 
