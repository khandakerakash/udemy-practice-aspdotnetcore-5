using System;

namespace Utils.AppExceptions
{
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException(string message) : base(message)
        {
        }
    }
}