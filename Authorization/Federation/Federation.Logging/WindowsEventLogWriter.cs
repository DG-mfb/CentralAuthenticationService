﻿using System;
using Kernel.Logging;

namespace Federation.Logging
{
    internal class WindowsEventLogWriter : AbstractLogWriter
    {
        public override void WriteExeption(object o)
        {
            //ToDo: Implement logging
            //if (o is Exception)
            //    LoggerManager.WriteExceptionToEventLog((Exception)o);
        }
    }
}