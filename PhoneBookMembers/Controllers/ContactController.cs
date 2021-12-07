using PhoneBookMembers.Models;
using PhoneBookMembers.Utilites;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBookMembers.Controllers
{
    public class ContactController
    {
        public Contact CurrentContact { get; }
        public List<Contact> Contacts { get; }
        public bool IsNewContact { get; } = false;
        private readonly User _currentUser;
        public ContactController(string userPhonenumber, string contactPhonenumber)
        {
            var users = GetUsersData().Result;
            _currentUser = users.FirstOrDefault(x => x.Phonenumber == userPhonenumber);
            Contacts = GetContactsData().Result;
            CurrentContact = Contacts.FirstOrDefault(x => x.Phonenumber == contactPhonenumber);
            if (CurrentContact is null)
            {
                CurrentContact = new Contact(contactPhonenumber);
                IsNewContact = true;
            }
        }
        public ContactController(string userPhonenumber)
        {
            var users = GetUsersData();
            _currentUser = users.Result.FirstOrDefault(x => x.Phonenumber == userPhonenumber);
        }
        public async Task SetNewContactData(string name, string secondName, string gender)
        {
            CurrentContact.Name = name;
            CurrentContact.SecondName = secondName;
            CurrentContact.Gender = gender;
            await Save(Contacts);
        }
        public async Task AddContact(string name, string secondName, string gender)
        {
            CurrentContact.Name = name;
            CurrentContact.SecondName = secondName;
            CurrentContact.Gender = gender;
            Contacts.Add(CurrentContact);
            await Save(Contacts);
        }
        public IEnumerable<Contact> GetContacts()
        {
            var data = GetContactsData();
            foreach (var contact in data.Result)
            {
                yield return contact;
            }
        }

        public async Task DeleteContact(string phonenumber)
        {
            var contact = Contacts.FirstOrDefault(x => x.Phonenumber == phonenumber);
            Contacts.Remove(contact);
            await Save(Contacts);
        }
        private async Task<List<User>> GetUsersData()
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
        private async Task<List<Contact>> GetContactsData()
        {
            using var file = new FileStream(@"~\Contacts\contacts_" + $"{_currentUser.Phonenumber}.json", FileMode.OpenOrCreate);
            try
            {
                return await JsonSerializer.DeserializeAsync<List<Contact>>(file);
            }
            catch
            {
                return new List<Contact>();
            }
        }
        private async Task Save(List<Contact> contacts)
        {
            await Cleaner();
            using var fs = new FileStream(@"~\Contacts\contacts_" + $"{_currentUser.Phonenumber}.json", FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(fs, contacts);
        }
        private async Task Cleaner()
        {
            string value = await File.ReadAllTextAsync(@"~\Contacts\contacts_" + $"{_currentUser.Phonenumber}.json");
            var point = 0;
            var charValue = value.ToCharArray();
            if (!string.IsNullOrWhiteSpace(value))
            {
                await Task.Run(() =>
                {
                for (int i = 0; i <= value.Length; i++)
                {
                    if (charValue[i] == ']')
                    {
                        point = i;
                        break;
                    }
                }
                });


                var notCorrectValue = value.Length - point;
                await Task.Run(() =>
                {
                    for (int i = point; i < notCorrectValue; i++)
                    {
                        charValue[i] = ' ';
                    }
                });
             

                await File.WriteAllTextAsync(@"~\Contacts\contacts_" + $"{_currentUser.Phonenumber}.json", charValue.ToString());
            }

        }
    }
}
