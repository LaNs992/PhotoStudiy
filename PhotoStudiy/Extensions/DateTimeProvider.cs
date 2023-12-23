using PhotoStudiy.Common.Entity;

namespace PhotoStudiy.API.Extensions
{
    /// <summary>
    /// Реализация <see cref="IDateTimeProvider"/>
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
