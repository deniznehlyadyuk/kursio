using Kursio.Common.Domain;

namespace Kursio.Common.Application.Exceptions;

public sealed class KursioException : Exception
{
    public KursioException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
