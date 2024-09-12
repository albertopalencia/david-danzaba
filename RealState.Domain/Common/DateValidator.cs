using RealState.Domain.Exceptions;

namespace RealState.Domain.Common
{
    public static class DateValidator
    {
        public static DateTime ValidateDatePassed(this DateTime value, string message)
        {
            if (value < DateTime.UtcNow.Date )
            {
                throw new CoreBusinessException(message);
            }
            return value;
        }

    }
}
