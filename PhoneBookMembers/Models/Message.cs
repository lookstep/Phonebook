using System.Text.Json.Serialization;

namespace PhoneBookMembers.Models
{
    public class Message
    {
        private string _sender;
        private string _listener;
        private string _messageData;
        [JsonInclude]
        public string Sender 
        {
            get => _sender;
            set => Set(ref _sender, value);
        }
        [JsonInclude]
        public string Listener 
        {
            get => _listener;
            set => Set(ref _listener, value);
        }
        [JsonInclude]
        public string MessageData
        {
            get => _messageData;
            set => Set(ref _messageData, value);
        }
        [JsonConstructor]
        public Message(string sender, string listener)
        {
            _sender = sender;
            _listener = listener;
        }
        private bool Set<T>(ref T field, T value)
        {
            if (value is string)
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                    return false;
                field = value;
                return true;
            }
            if (field.Equals(value)) return false;
            field = value;
            return true;
        }

    }
}
