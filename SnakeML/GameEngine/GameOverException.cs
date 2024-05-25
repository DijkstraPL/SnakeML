using System.Runtime.Serialization;

namespace SnakeML.GameEngine
{
    [Serializable]
    internal class GameOverException : Exception
    {
        public GameOverException()
        {
        }

        public GameOverException(string? message) : base(message)
        {
        }

        public GameOverException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GameOverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
