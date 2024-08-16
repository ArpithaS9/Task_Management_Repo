using System.Runtime.Serialization;

namespace Task_Mangement.Repository
{
    [Serializable]
    internal class CustomException : Exception
    {
        public CustomException()
        {
        }

        public CustomException(string? message) : base(message)
        {
        }

    }
}