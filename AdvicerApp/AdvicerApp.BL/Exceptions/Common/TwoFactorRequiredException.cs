using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class TwoFactorRequiredException : Exception, IBaseException
{

    public int StatusCode => StatusCodes.Status401Unauthorized;

    public string ErrorMessage { get; }
    public TwoFactorRequiredException()
    {
        ErrorMessage = "Two-factor authentication is required";
    }

    public TwoFactorRequiredException(string message) : base(message)
    {
        ErrorMessage = message;
    }


}
