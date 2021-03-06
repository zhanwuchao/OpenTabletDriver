using System;

namespace TabletDriverPlugin.Logging
{
    public class ExceptionLogMessage : LogMessage
    {
        public ExceptionLogMessage(Exception ex) : base(ex.GetType().FullName, ex.Message, true)
        {
            Exception = ex;
        }

        public Exception Exception { private set; get; }
    }
}