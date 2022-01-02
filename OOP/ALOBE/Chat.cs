using System.Collections.Generic;
using System;
namespace alobe
{
    public class Chat: Account, IView, IData
    {
        public List<User> members=new List<User>();
        public List<Message> mess=new List<Message>();
        public Chat(){}
        ~Chat(){}
        public Chat(string ID)
        {
            this.ID = ID;
            readFile();
        }
        public Chat(string member1, string member2)
        {
            User u1=new User(member1);
            members.Add(u1);
            User u2=new User(member2);
            members.Add(u2);
            u1.addChat(ID);
            u2.addChat(ID);
            this.ID = createID();
        }
        public void display()
        {
            Console.WriteLine(ID);
            Console.WriteLine("THANH VIEN");
            foreach (User i1 in members)
            {
                Console.WriteLine("{0,-10}: {1,-30}",i1.ID,i1.name);
            }
            Console.WriteLine("NOI DUNG");
            foreach (Message i2 in mess)
            {
                i2.display();
            }
            newMessage();
        }
        public void newMessage()
        {
            Console.WriteLine("Nhap tin nhan (0 de thoat): ");
            string temp=Console.ReadLine();
            if (temp=="0")
            {
                writeFile();
                Console.Clear();
                return;
            }
            mess.Add(new Message(members[0],members[1],temp));
            Console.Clear();
            display();
        }
        public void readFile()
        {
            string []temp=support.readFile($"data/chat/{ID}");
            int t=0;
            foreach (string i in temp)
            {
                if (t>=2)
                {
                    Message mtemp=new Message(sender:new User(i.Split("*-*")[1]),receiver:new User(i.Split("*-*")[2]),ibirthday:i.Split("*-*")[3],content:i.Split("*-*")[0]);
                    mess.Add(mtemp);
                }
                else
                {
                    User utemp=new User(i);
                    members.Add(utemp);
                }
                t++;
            }
        }
        public void writeFile()
        {
            support.writeFile($"data/chat/{ID}",members[0].ID);
            support.appendFile($"data/chat/{ID}",members[1].ID);
            foreach(Message m in mess)
            {
                support.appendFile($"data/chat/{ID}",$"{m.content}*-*{m.sender.ID}*-*{m.receiver.ID}*-*{m.birthday.Day}/{m.birthday.Month}/{m.birthday.Year}");
            }
        }
        public string createID()
        {
            bool t1=support.checkExist($"data/chat/{members[0].ID}{members[1].ID}");
            bool t2=support.checkExist($"data/chat/{members[1].ID}{members[0].ID}");
            if ((t1==false && t2==false) && t1==true)
            {
                return members[0].ID+members[1].ID;
            }
            return members[1].ID+members[0].ID;
        }
    }
}