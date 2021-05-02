using System;

namespace exceptions
{
    public class EmptyDeckException: ApplicationException
    {
        private readonly string _error;

        public EmptyDeckException() { }
        
        public EmptyDeckException(string msg) :base(msg)
        {
            _error = msg;
        }
        
        public EmptyDeckException(string msg, Exception innerException):base(msg,innerException)
        {
            _error = msg;
        }
        
        public string GetError()
        {
            return _error;
        }
    }
}