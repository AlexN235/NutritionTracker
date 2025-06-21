
namespace NutruitionTracker
{
    [Serializable]
    internal class InvalidPropertyForDatabaseQuery : Exception
    {
        public InvalidPropertyForDatabaseQuery()
        {
        }

        public InvalidPropertyForDatabaseQuery(string? message) : base(message)
        {
        }

        public InvalidPropertyForDatabaseQuery(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}