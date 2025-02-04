﻿using Microsoft.AspNetCore.Http;

namespace AdvicerApp.BL.Exceptions.Common;

public class NotFoundException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound;

    public string ErrorMessage { get; }
    public NotFoundException(string message)
    {
        ErrorMessage = message;
    }
    public NotFoundException()
    {
        ErrorMessage = "Not Found";
    }
}
public class NotFoundException<T> : NotFoundException, IBaseException
{
    public NotFoundException() : base(typeof(T).Name + "not found") { }
    public NotFoundException(string message) : base(message) { }

}
