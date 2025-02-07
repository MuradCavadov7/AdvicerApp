using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class InvalidScoreException : Exception, IBaseException
{

    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public InvalidScoreException(string message) : base(message)
    {
        ErrorMessage = message;
    }

}
