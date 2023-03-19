using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace LogComponent
{
    public class AsyncLog : ILog
    {
        private readonly Thread _runThread;
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly BlockingCollection<LogLine> _logQueue;
        private readonly IPersistency _persistency;
        private bool _exit = false;


        public AsyncLog(IPersistency type)
        {
            _persistency = type;
            _logQueue = new BlockingCollection<LogLine>(new ConcurrentQueue<LogLine>());

            this._runThread = new Thread(this.LoggingLoop);
            this._runThread.Start();
        }

        private void LoggingLoop()
        {
            while(!_exit)
            {
                foreach (var item in _logQueue)
                {
                    if (_tokenSource.IsCancellationRequested)
                        return;

                    LogLine currentLog; 
                    try
                    {
                        currentLog = _logQueue.Take(_tokenSource.Token);
                        _persistency.Save(currentLog);
                    }
                    catch (OperationCanceledException e) {
                        //handle e
                    }
                    Thread.Sleep(500);
                }   
            }
        }

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

        public void WriteSingleItem(string text)
        {
            Task t1 = Task.Run(() =>
            {
                this._logQueue.Add(new LogLine() { Text = text, Timestamp = DateTime.Now });
            });
        }

        public void WriteMultipleItems(List<string> items)
        {
            //** Not realy necessary for our case,
            //** but WriteSingleItem makes a single task. This method can handle multiple items in 1 task
            Task producerThread = Task.Factory.StartNew(() =>
            {
               foreach (string item in items)
                { 
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    this._logQueue.Add(new LogLine() { Text = item, Timestamp = DateTime.Now });
                }
            });
        }
    }
}