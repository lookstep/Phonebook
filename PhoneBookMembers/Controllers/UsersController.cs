using PhoneBookMembers.Utilites;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookMembers.Controllers
{
    
    public class UsersController
    {
        public User CurrentUser { get; }
        public List<User> Users { get; }
        public bool IsNewUser { get; } = false;
        private bool canMemberCall;
        public UsersController(string phonenumber)
        {
            Users = GetData().Result;
            CurrentUser = Users.FirstOrDefault(x => x.Phonenumber == phonenumber);
            if(CurrentUser is null)
            {
                CurrentUser = new User(phonenumber);
                IsNewUser = true;
            }
        }
        public async Task SetNewUsersDate(string name, string secondName, string gender, int balance)
        {
            CurrentUser.Name = name;
            CurrentUser.SecondName = secondName;
            CurrentUser.Gender = gender;
            CurrentUser.Balance = balance;
            Users.Add(CurrentUser);
            await Save(Users);
        }
        private async Task Save(List<User> contacts)
        {
            using var file = new FileStream(Constants.usersDataFile, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(file, contacts);
        }

        private async Task<List<User>> GetData()
        {
            using var file = new FileStream(Constants.usersDataFile, FileMode.OpenOrCreate);
            try
            {
                return await JsonSerializer.DeserializeAsync<List<User>>(file);
            }
            catch
            {
                return new List<User>();
            }
        }
        public string CheakYourselfInformation() => $"Имя: {CurrentUser.Name} \nФамилия: {CurrentUser.SecondName} \nПол: {CurrentUser.Gender} \nНомер телефона: {CurrentUser.Phonenumber} \nБаланс: {CurrentUser.Balance}";
        public async Task<int> AddBalance(int addValue)
        {
            CurrentUser.Balance += addValue;
            await Save(Users);
            return CurrentUser.Balance;
        }
    }
}
