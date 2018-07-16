using System;
using System.Diagnostics;
using Kernel.Extensions;
using Kernel.Logging;

namespace Federation.Logging
{
    public class WindowsEventLogLogger : ILogProvider
    {
        private ILogWriter _logWriter;
        public WindowsEventLogLogger(ILogWriter logWriter)
        {
            this._logWriter = logWriter;
        }

        public ILogWriter LogWriter
        {
            get => this._logWriter;
            set => this._logWriter = value;
        }

        public string AddParameter(string name, object value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public void LogMessage(string m)
        {
            
        }

        public bool TryCommitException(object o, out Exception exception)
        {
            exception = null;
            return true;
        }

        public bool TryLogException(Exception ex, out Exception result)
        {
            var details = ExceptionExtensions.BuildExceptionStringRecursively(ex);
            Debug.WriteLine(details);
            result = ex;
            return true;
        }
    }
}