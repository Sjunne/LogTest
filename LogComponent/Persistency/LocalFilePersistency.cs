using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent
{
    public class LocalFilePersistency : IPersistency
    {
        private string _fileDestination;
        private StreamWriter _writer;
        private string _LastFileCreationdate;
        private string _filename;

        public LocalFilePersistency(string fileDestination)
        {
            this._fileDestination = fileDestination;
            _filename = _fileDestination + "/" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log";
            File.Create(_filename).Dispose();
            this._LastFileCreationdate = DateTime.Now.ToShortDateString();
        }


        public void Save(LogLine log)
        {
           
            var exists = File.Exists(_filename);

            if(exists && DateTime.Now.ToShortDateString().Equals(this._LastFileCreationdate))
            {
                
                this._writer = File.AppendText(_filename);
                this._writer.AutoFlush = true;
                this._writer.Write(FormatItem(log));
                this._writer.Dispose();
            }
            else
            {
                this._writer = File.AppendText(@"C:\LogTest\Log" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log");
                _filename = _fileDestination + "/" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log";
                this._LastFileCreationdate = DateTime.Now.ToShortDateString();
                this._writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
                this._writer.AutoFlush = true;
                this._writer.Write(FormatItem(log));
                this._writer.Dispose();
            }
        }


        private string FormatItem(LogLine item)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(item.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            stringBuilder.Append("\t");
            stringBuilder.Append(item.LineText());
            stringBuilder.Append("\t");
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }
    }
}
