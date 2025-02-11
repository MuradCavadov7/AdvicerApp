using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class UnAuthorizedAccessException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status401Unauthorized;

    public string ErrorMessage { get; }
    public UnAuthorizedAccessException()
    {
        ErrorMessage = "User not logged in";
    }
    public UnAuthorizedAccessException(string message) : base(message)
    {
        ErrorMessage = message;
    }

}