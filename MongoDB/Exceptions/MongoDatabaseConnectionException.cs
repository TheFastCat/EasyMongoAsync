﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
    public class MongoDatabaseConnectionException : Exception
    {
        public MongoDatabaseConnectionException(string message)
            : base(message)
        {
        }

        public MongoDatabaseConnectionException(string message, Exception ex)
            : base(message, ex)
        {
        }
    } 
}
