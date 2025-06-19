using System;

namespace RawDeal
{
    public class UnexpectedBehaviorException : Exception
    {
        public UnexpectedBehaviorException(string message) : base(message)
        {
        }
    }
}