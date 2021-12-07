using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using PhoneBookMembers.Utilites;
using System.IO;
using PhoneBookMembers.Models;

namespace PhoneBookMembers.Controllers
{
    public class MessageController
    {
        public User Sender { get; }
        public User Listener { get; }
        public Message CurrentMessage { get; }
        public List<Message> Messages { get; }
        
        public MessageController(string phoneNumberListener, string phoneNumberSender)
        {
            var users = GetData().Result;
            Listener = users.FirstOrDefault(x => x.Phonenumber == phoneNumberListener);
            Sender = users.FirstOrDefault(x => x.Phonenumber == phoneNumberSender);
            Messages = GetMessageData().Result;
            CurrentMessage = new Message(phoneNumberSender, phoneNumberListener);
        }

        public IEnumerable<Message> GetMessage()
        {
            var data = GetMessageData().Result;
            foreach (var contact in data)
            {
                yield return contact;
            }
        }
        private async Task<List<Message>> GetMessageData()
        {
            using var file = new FileStream(@"~\Message\message_to" + $"{Listener.Phonenumber}.json", FileMode.OpenOrCreate);
            try
            {
                return await JsonSerializer.DeserializeAsync<List<Message>>(file);
            }
               catch
            {
                return new List<Message>();
            }
        }
        public async Task SendMessage(string message)
        {
            CurrentMessage.Listener = Listener.Phonenumber;
            CurrentMessage.Sender = Sender.Phonenumber;
            CurrentMessage.MessageData = message;
            Messages.Add(CurrentMessage);
            await SaveMessage(Messages);
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
        private async Task SaveMessage(List<Message> message)
        {
            
            using var file = new FileStream(@"~\Message\message_to" + $"{Listener.Phonenumber}.json", FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(file, message);
        }
        private async Task Cleaner()
        {
            string value = await File.ReadAllTextAsync(@"~\Message\message_to" + $"{Listener.Phonenumber}.json");
            if (string.IsNullOrWhiteSpace(value))
                return;
            var point = 0;
            var charValue = value.ToCharArray();
            await Task.Run(() =>
            {
                for (int i = 0; i <= charValue.Length; i++)
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
            }
            );

            await File.WriteAllTextAsync(@"~\Message\message_to" + $"{Listener.Phonenumber}.json", charValue.ToString());
        }
    }
}
