namespace LogComponent
{
    public interface ILog
    {
        /// <summary>
        /// Stop the logging. If any outstadning logs theses will not be written to Log
        /// </summary>
        void StopWithoutFlush();

        /// <summary>
        /// Stop the logging. The call will not return until all all logs have been written to Log.
        /// </summary>
        void StopWithFlush();

        /// <summary>
        /// Write a message to the Log.
        /// </summary>
        /// <param name="text">The text to be written to the log</param>
        void WriteSingleItem(string text);
        /// <summary>
        /// Write a list of messages to the Log.
        /// </summary>
        /// <param name="text">The list of text to be written to the log</param>
        void WriteMultipleItems(List<string> items);
    }
}
