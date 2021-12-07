using PhoneBookMembers;
using System.Runtime.Serialization.Json;
using PhoneBookMembers.Controllers;
using System.Collections.Generic;
using System;
using PhoneBookMembers.Utilites;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ViewPhoneBookMembers
{
    class Program
    {
        static async Task Main(string[] args)
        {
         
        Console.WriteLine("Введите номер телефона: ");
            var phonenumber = Console.ReadLine();
            var user = new UsersController(phonenumber);
            if (user.IsNewUser)
            {
                Console.WriteLine("Загерестрируйтесь: ");
                Console.Write("\t Введите имя: ");
                string name = Console.ReadLine();
                Console.Write("\t Введите фамилию: ");
                string secondName = Console.ReadLine();
                Console.Write("\t Введите пол: ");
                string gender = Console.ReadLine();
                Console.Write("\t Введите ваш баланс: ");
                int balance = int.Parse(Console.ReadLine());
                user.SetNewUsersDate(name, secondName, gender, balance);
            }
            Console.Clear();
            Console.WriteLine($"Вы вошли как пользователь с номером {user.CurrentUser.Phonenumber}");
            Console.WriteLine("Список действий: ");
            Console.WriteLine(
                "1) Узнать информацию о себе \n" +
                "2) Добавить контакт \n" +
                "3) Список ваших контактов \n" +
                "4) Написать письмо \n" +
                "5) Посмотреть ваши письма \n" +
                "6) Изменить данные контакта \n" +
                "7) Пополнить баланс \n" +
                "8) Удалить контакт \n" +
                "9) Выйти \n");
            var chouse = int.Parse(Console.ReadLine());
            switch (chouse)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine(user.CheakYourselfInformation()); 
                    break;
                case 2:            
                    Console.Clear();
                    Console.WriteLine("Добавление контакта: ");
                    Console.Write("\t Введите номер телефона: ");
                    var contactsPhonenumber = Console.ReadLine();
                    var contact = new ContactController(phonenumber, contactsPhonenumber);
                    if (user.CurrentUser.Phonenumber != contactsPhonenumber)
                    {
                        Console.Write("\t Введите имя контакта: ");
                        string name = Console.ReadLine();
                        Console.Write("\t Введите фамилию контакта: ");
                        string secondName = Console.ReadLine();
                        Console.Write("\t Введите пол контакта: ");
                        string gender = Console.ReadLine();
                        await contact.AddContact(name, secondName, gender);
                    }
   
                    else
                        Console.WriteLine("Вы не можите добавить сами себя");
                        Console.WriteLine($"Контакт с номером телефона {contactsPhonenumber} успешно добавлен.");
                    
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Все ваши контакты: ");
                    contact = new ContactController(phonenumber);
                    int i = 1;
                    foreach (var el in contact.GetContacts())
                    {  
                        Console.WriteLine($"{i} контакт\n\tИмя: {el.Name}\n\tФамилия контакта: {el.SecondName}\n\tПол контакта: {el.Gender}\n\tНомер телефона контакта: {el.Phonenumber}");
                        i++;
                    }
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Кому пишите: ");
                    var phoneNumber = Console.ReadLine();
                    var messageController = new MessageController(phoneNumber, user.CurrentUser.Phonenumber);
                    Console.WriteLine("Сооьщение: ");
                    var message = Console.ReadLine();
                    await messageController.SendMessage(message);
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Введите от какого номера телефона вы хотите увидеть письма:  ");
                    var sender = Console.ReadLine();
                    
                    messageController = new MessageController(phonenumber, sender);
                    
                    foreach (var el in messageController.GetMessage())
                    {
                        Console.WriteLine($"{el.Listener}\n {el.Sender}\n {el.MessageData} \n");
                    }
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Введите номер телефона контакта, которого хотите изменить: ");                   
                    contactsPhonenumber = Console.ReadLine();
                    contact = new ContactController(phonenumber, contactsPhonenumber);
                    Console.Write("\t Введите имя контакта: ");
                    string newName = Console.ReadLine();
                    Console.Write("\t Введите фамилию контакта: ");
                    string newSecondName = Console.ReadLine();
                    Console.Write("\t Введите пол контакта: ");
                    string newGender = Console.ReadLine();
                    await contact.SetNewContactData(newName, newSecondName, newGender);
                    break;
                case 7:
                    Console.Clear();
                    Console.Write("Введите сумму, на которую хотите пополнить баланс: ");
                    int add = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Баланс пополнен. Теперь у вас на счету {user.AddBalance(add).Result}");
                    break;
                case 8:
                    Console.Clear();
                    Console.Write("Введите номер телефона: ");
                    contactsPhonenumber = Console.ReadLine();
                    contact = new ContactController(phonenumber, contactsPhonenumber);
                    await contact.DeleteContact(contactsPhonenumber);
                    Console.WriteLine("Контакт был успешкно удалён.");
                    break;
                case 9:
                    break;


            }
        }

    }
}
