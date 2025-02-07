using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class InvalidFileException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public InvalidFileException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
