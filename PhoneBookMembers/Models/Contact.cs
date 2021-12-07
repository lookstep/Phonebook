using System.Text.Json.Serialization;

namespace PhoneBookMembers.Models
{
    
    public class Contact
    {
        private string _name;
        private string _secondName;
        private string _gender;
        private string _phonenumber;
        [JsonInclude]
        public string Name 
        {
            get => _name;
            internal set => Set(ref _name, value); 
        }
        [JsonInclude]
        public string SecondName 
        {
            get => _secondName;
            internal set => Set(ref _secondName, value);
        }
        [JsonInclude]
        public string Gender
        {
            get => _gender;
            internal set => Set(ref _gender, value);
        }
        [JsonInclude]
        public string Phonenumber 
        {
            get => _phonenumber;
            internal set => Set(ref _phonenumber, value);
        }
        [JsonConstructor]
        internal Contact(string phonenumber)
        {
            _phonenumber = phonenumber;
        }

        public Contact()
        {
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
