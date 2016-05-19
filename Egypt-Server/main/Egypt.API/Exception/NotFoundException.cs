using System;

namespace Egypt.API.Exception
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            :base(message)
        {
            
        }
    }
}