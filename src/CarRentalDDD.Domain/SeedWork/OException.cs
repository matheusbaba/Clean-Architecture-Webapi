using System;

namespace CarRentalDDD.Domain.SeedWork
{

    /// <summary>
    /// Used to mark exceptions. Exceptions of this type means that they are a threatened error
    /// </summary>
    public interface IOException
    {
    }

    /// <summary>
    /// Represents a threatened error
    /// </summary>
    public class OException : Exception, IOException
    {
        public OException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Represents a threatened Argument Null error
    /// </summary>
    public class OArgumentNullException : ArgumentNullException, IOException
    {
        public OArgumentNullException(string arg)
            :base(arg, $"{arg} cannot be null")
        {
        }
    }
    /// <summary>
    /// Represents a threatened Not Found error 
    /// </summary>
    public class ONotFoundException : Exception, IOException
    {
        public ONotFoundException(string arg)
            : base ($"{arg} was not found")
        {

        }
    }
    /// <summary>
    /// Represents a threatened Invalid Argument error
    /// </summary>
    public class OInvalidArgumentException : ArgumentException, IOException
    {
        public OInvalidArgumentException(string arg)
            : base($"{arg} is invalid", arg)
        {

        }
    }
}
