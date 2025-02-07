using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

internal class UserHasNoPermissionException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status403Forbidden;

    public string ErrorMessage { get; }
    public UserHasNoPermissionException()
    {
        ErrorMessage = "User has no permission";
    }

    public UserHasNoPermissionException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
