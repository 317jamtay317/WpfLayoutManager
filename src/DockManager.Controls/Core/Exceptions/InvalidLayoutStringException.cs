using System;

namespace DockManager.Controls.Core.Exceptions;

public class InvalidLayoutStringException : ArgumentException
{
    public InvalidLayoutStringException(string layoutString)
      :base($"The Layout String Provided ({layoutString}) is invalid, " +
            $"Please ensure that it is comma seperated and " +
            $"only contains and empty string star value(2* || *) " +
            $"or a numerical value")
    {
    }
}