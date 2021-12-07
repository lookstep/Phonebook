using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devolpmentTest
{
    [DataContract]
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
        [DataMember]
        public string Name
        {
            get => _name;
            internal set => Set(ref _name, value);
        }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [DataMember]
        public string SecondName
        {
            get => _secondName;
            internal set => Set(ref _secondName, value);
        }
        /// <summary>
        /// Отчество (если оно существует)
        /// </summary>
#nullable enable
        [DataMember]
        public string? Gender
        {
            get => _gender;
            internal set => Set(ref _gender, value);
        }
#nullable disable
        /// <summary>
        /// Номер телефона
        /// </summary>
        [DataMember]
        public string Phonenumber
        {
            get => _phoneNumber;
            internal set => string.Format("{0:+7(###)###-##-##}", Set(ref _phoneNumber, value));
        }
        /// <summary>
        /// Баланс
        /// </summary>
        [DataMember]
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
        public User()
        {

        }

        internal User(string name, string secondName, string gender, string phonenumber, int balance)
        {
            _name = name;
            _secondName = secondName;
            _gender = gender;
            _phoneNumber = phonenumber;
            _balance = balance;

        }

        public override string ToString()
        {
            return Name;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
