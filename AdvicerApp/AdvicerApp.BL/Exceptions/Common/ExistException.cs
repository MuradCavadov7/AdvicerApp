using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class ExistsException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status409Conflict;

    public string ErrorMessage { get; }
    public ExistsException(string message)
    {
        ErrorMessage = message;
    }
    public ExistsException()
    {
        ErrorMessage = "Is Exist";
    }
}
public class ExistsException<T> : ExistsException, IBaseException
{
    public ExistsException() : base(typeof(T).Name + "is exist") { }
    public ExistsException(string message) : base(message) { }
}
