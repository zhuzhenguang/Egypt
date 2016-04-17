using System;

namespace Egypt.API.Exception
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}