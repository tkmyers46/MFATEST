using System;
using System.Diagnostics;

namespace TestLog
{
    public class TestLogger
    {
        /// <summary>
        /// Writes to the log provided and assigns log type
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logType"></param>
        /// <param name="logName"></param>
        public static void WriteLog(string message, EventLogEntryType logType, LogName logName)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);

            string logNameString = logName.ToString();

            if (!EventLog.SourceExists(logNameString))
            {
                EventLog.CreateEventSource(logNameString, logNameString);
            }

            EventLog newLog = new EventLog(logNameString);
            object something = newLog.Source;
            newLog.Source = logNameString;
            EventLog.WriteEntry(logNameString, message, logType);
        }
    }
}
