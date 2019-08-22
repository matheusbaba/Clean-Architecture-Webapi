using System;

namespace CarRentalDDD.Domain.SeedWork
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {

        }

        public static CustomException Exception(string message)
        {
            return new CustomException(message);
        }

        public static CustomException NullArgument(string arg)
        {
            return new CustomException($"{arg} cannot be null");
        }

        public static CustomException InvalidArgument(string arg)
        {
            return new CustomException($"{arg} is invalid");
        }

        public static CustomException NotFound(string arg)
        {
            return new CustomException($"{arg} was not found");
        }
    }
}
