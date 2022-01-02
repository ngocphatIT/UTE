using System;
using System.Collections.Generic;
namespace alobe
{
    public class Message: IView
    {
        public User sender;
        public User receiver;
        public string content;
        public DateTime birthday=new DateTime();
        public Message(){}
        public Message(User sender, User receiver, string content)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.content = content;
            this.birthday = DateTime.Today;
        }
        public Message(User sender, User receiver, DateTime birthday,string content)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.content = content;
            this.birthday = birthday;
        }
        public Message(User sender, User receiver, string ibirthday,string content)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.content = content;
            this.birthday=new DateTime(Int32.Parse(ibirthday.Split("/")[2]),Int32.Parse(ibirthday.Split("/")[1]),Int32.Parse(ibirthday.Split("/")[0]));
        }
        public void display()
        {
            Console.WriteLine($" {content} *-* {birthday.Day}/{birthday.Month}/{birthday.Year} *-* {sender.name} *-* {receiver.name} ");
        }
        ~Message(){}
    }
}