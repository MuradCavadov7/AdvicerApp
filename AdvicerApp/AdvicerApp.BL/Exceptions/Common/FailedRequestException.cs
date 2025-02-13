using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class FailedRequestException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status417ExpectationFailed;

    public string ErrorMessage { get; }
    public FailedRequestException(string message)
    {
        ErrorMessage = message;
    }
}
