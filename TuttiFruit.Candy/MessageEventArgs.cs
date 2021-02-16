namespace TuttiFruit.Candy
{
    public class MessageEventArgs
    {
        public object Message { get; internal set; }

        public MessageEventArgs(object message)
        {
            Message = message;
        }
    }
}