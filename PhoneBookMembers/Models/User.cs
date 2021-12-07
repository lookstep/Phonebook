using System.Text.Json.Serialization;

namespace PhoneBookMembers
{
    
    public class User
    {
        #region PrivatField
        private string _name;
        private string _secondName;
#nullable enable
        private string? _gender;
#nullable disable
        private string _phoneNumber;
        private int _balance;
        #endregion

        #region Property
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonInclude]
        public string Name 
        {
            get => _name;
            internal set => Set(ref _name, value);
        }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [JsonInclude]
        public string SecondName
        {
            get => _secondName;
            internal set => Set(ref _secondName, value);
        }
        /// <summary>
        /// Отчество (если оно существует)
        /// </summary>
#nullable enable
        [JsonInclude]
        public string? Gender 
        {
            get => _gender;
            internal set => Set(ref _gender, value);
        }
#nullable disable
        /// <summary>
        /// Номер телефона
        /// </summary>
        [JsonInclude]
        public string Phonenumber 
        {
            get => _phoneNumber;
            internal set  => Set(ref _phoneNumber, value);                                    
        }
        /// <summary>
        /// Баланс
        /// </summary>
        [JsonInclude]
        public int Balance
        {
            get => _balance;
            internal set => Set(ref _balance, value);            
        }
        #endregion        
        /// <summary>
        /// Метод, позволяющий устанавливать значения в стринговые поля, проверяя их на null или на пустоту
        /// </summary>
        /// <param name="field">изменяемое поле</param>
        /// <param name="value">значение</param>
        /// <returns>Возможность установить значение в поле</returns>
        private bool Set<T>(ref T field, T value)
        {
            if(value is string)
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
        public User()
        {
            
        }
        [JsonConstructor]
        public User(string phonenumber)
        {
            _phoneNumber = phonenumber;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
