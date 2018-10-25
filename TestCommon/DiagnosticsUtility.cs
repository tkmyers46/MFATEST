using System;
using System.Diagnostics;

namespace TestCommon
{
    public class DiagnosticsUtility
    {
        public static EventLogEntryType LogEntryType { get; set; }

        public EventLogEntryType EventLogEntryType()
        {
            return LogEntryType;
        }
        
        public static string EventLogSource(EventLog eventLog, string source)
        {
            return eventLog.Source = source;
        }
        
        /// <summary>
        /// Writes to the eventlog
        /// </summary>
        /// <param name="eventlog"></param>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        public void WriteEventLog(EventLog eventlog, string source, string message, EventLogEntryType entryType)
        {
            eventlog.Source = EventLogSource(eventlog, source);

            switch(entryType)
            {
                case System.Diagnostics.EventLogEntryType.Error:
                    eventlog.WriteEntry(message, entryType);
                    break;
                case System.Diagnostics.EventLogEntryType.Warning:
                    eventlog.WriteEntry(message, entryType);
                    break;
                case System.Diagnostics.EventLogEntryType.Information:
                    eventlog.WriteEntry(message, entryType);
                    break;
                case System.Diagnostics.EventLogEntryType.FailureAudit:
                    eventlog.WriteEntry(message, entryType);
                    break;
                case System.Diagnostics.EventLogEntryType.SuccessAudit:
                    eventlog.WriteEntry(message, entryType);
                    break;
                default:
                    Console.WriteLine("Error: TestLog entry failed for " + message + " from " + source);
                    break;
            }
            Console.WriteLine("Success: TestLog entry in log '{0}' ", eventlog.Log);
        }
    }
}
