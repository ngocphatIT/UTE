using System.IO;
using System;
using System.Collections.Generic;
namespace alobe
{
    public class User: Account, IOperation, IReact, IUser, IView, IData
    {
        public string user;
        public string password;
        public List<string> friend=new List<string>();
        public Question question;
        public List<User> userList=new List<User>();
        public Chat mainChat;
        public User(){}
        public User(string ID)
        {
            this.ID=ID;
            readFile();
        }
        public User(string user, string password)
        {
            this.user = user;
            this.password = password;
        }
        public void update(int choice, string data)
        {

            if (choice!=0)
            {

                switch(choice)
                {
                    case 1:
                        this.name=data;
                        break;
                    case 2:
                        this.birthday=new DateTime(Int32.Parse(data.Split(" ")[2]),Int32.Parse(data.Split(" ")[1]),Int32.Parse(data.Split(" ")[0]));
                        break;
                    case 3:
                        this.info.name=data;
                        break;
                    case 4:
                        this.info.address=data;
                        break;
                    case 5:
                        this.info.phone=data;
                        break;
                    case 6:
                        this.info.hobby=data;
                        break;
                    case 7:
                        this.info.gender=data;
                        break;                   
                }
                writeFile();
            }
        }
        public void lockAccount(){
            this.stateAccount=!this.stateAccount;
        }
        public void post()
        {
            Post ptemp=new Post();
            if (ptemp.createPost()!="-1")
            {
                IDPost.Add(ptemp.ID);
                ptemp.writeFile();
                writeFile();
            }
        }
        public override void login(string iusername, string ipassword)
        {
            foreach(User i in userList)
            {
                if (i.user==iusername && i.password==ipassword)
                {
                    ID=i.ID;
                    this.stateLogin=true;
                    break;
                }
            }
            Console.Clear();
            if (this.stateLogin==true)
            {
                readFile();
            }
        }
        public override void register(string iuser, string ipassword, string iname,Question iquestion)
        {
            this.ID=createID();
            this.user=iuser;
            this.password=ipassword;
            this.name=iname;
            this.question=iquestion;
            support.appendFile("UserIDList.dat",$"{ID}");
            writeFile();
        }
        public void changePassword(string password)
        {
            this.password=password;
            writeFile();
        }
        public void resetPassword(string ipassword)
        {
            this.password=ipassword;
            writeFile();

        }
        public bool findUser(string iID)
        {
            bool isFind=false;
            foreach(User i in userList)
            {
                if ((i.ID==iID || i.name==iID) && i.stateAccount==true)
                {
                    i.display();
                    isFind=true;
                }
            }
            return isFind;
        }
        public string createID()
        {
            string temp;
            Random rand = new Random();
            int num;
            do
            {
                temp="";
                for (int i = 0; i <9; i++)
                {
                    num=rand.Next(0,9);
                    temp+=num.ToString();
                }
            }
            while(File.Exists($"data/user/{ID}")==true);
            return temp;
            
        }
        public bool checkUser(string username)
        {
            foreach (User i in userList)
            {
                if (i.user==username)
                    return false;
            }
            return true;
        }
        public void readFile()
        {
            string[] temp=File.ReadAllLines($"data/user/{ID}");
            try {this.ID=temp[0];} catch{}
            try{this.user=temp[1];}catch{}
            try {this.password=temp[2];} catch{}
            try {this.name=temp[3];} catch{}
            try {this.question=new Question(temp[4],temp[5]);} catch{}
            try{this.friend.AddRange(temp[6].Split(','));}catch{}
            try{this.IDVisit.AddRange(temp[7].Split(','));}catch{}
            try{this.IDFollow.AddRange(temp[8].Split(','));}catch{}
            try{this.IDPost.AddRange(temp[9].Split(','));} catch{}
            try{this.IDChat.AddRange(temp[10].Split(','));} catch{}
            try{
                if (temp[11]=="0")
                    this.stateAccount=false;
                else
                    this.stateAccount=true;
            }
            catch{}
            try {this.info.name=temp[12];} catch{}
            try {this.info.hobby=temp[13];} catch{}
            try {this.info.phone=temp[14];} catch{}
            try {this.info.address=temp[15];} catch{}
            try {this.info.gender=temp[16];} catch{}
        }
        public void writeFile()
        {
            string ftemp="";
            string vtemp="";
            string fotemp="";
            string ptemp="";
            string ctemp="";
            int i=0;
            int fn=friend.Count;
            int vn=IDVisit.Count;
            int fon=IDFollow.Count;
            int pn=IDPost.Count;
            int cn=IDChat.Count;
            while (i<fn||i<vn||i<fon||i<pn||i<cn)
            {
                if (i<fn && friend[i]!="")
                    ftemp=ftemp+friend[i]+",";
                if (i<vn && IDVisit[i]!="")
                    vtemp=vtemp+IDVisit[i]+",";
                if (i<fon && IDFollow[i]!="")
                    fotemp=fotemp+IDFollow[i]+",";
                if (i<pn && IDPost[i]!="")
                    ptemp=ptemp+IDPost[i]+",";
                if (i<cn && IDChat[i]!="")
                    ctemp=ctemp+IDChat[i]+",";
                i++;
            }
            string state;
            if (stateAccount==false) state="0";
            else state="1";
            support.writeFile($"data/user/{ID}",$"{ID}"+"\n"+$"{user}"+"\n"+$"{password}"+"\n"+$"{name}"+"\n"+$"{question.question}"+"\n"+$"{question.answer}"+"\n"+$"{ftemp}"+"\n"+$"{vtemp}"+"\n"+$"{fotemp}"+"\n"+$"{ptemp}"+"\n"+$"{ctemp}"+"\n"+$"{state}"+"\n"+info.convertToString());
        }
        public void viewWall(string iID)
        {
            if (stateAccount==true)
            {
                User user=new User(iID);
                user.myWall();
            }
        }
        public void myWall()
        {
            Console.WriteLine("{0,-15} : {1}","ID",this.ID);
            Console.WriteLine("{0,-15} : {1}","Ten hien thi: ",this.name);
            Console.WriteLine("BAI VIET");
            support.print(104,"=");
            Console.WriteLine("| {0,-10}| {1,-10}| {2,-77}|","ID","Ngay dang","Noi dung");
            support.print(104,"-");
            foreach (string i in IDPost)
            {
                try{
                Post ptemp=new Post(i);
                ptemp.readFile();
                Console.WriteLine("| {0,-10}| {1,2}/{2,2}/{3,-4}| {4,-77}|",ptemp.ID,ptemp.birthday.Day, ptemp.birthday.Month, ptemp.birthday.Year,ptemp.content);
                }
                catch{}
            }
            support.print(104,"=");
            info.display();
        }
        public void addChat(string iID){
            IDChat.Add(iID);
            writeFile();
        }
        public void display()
        {
            Console.WriteLine("{0,-10} | {1,-30}",ID,name);
        }
 
        public void acceptVisit(string iID)
        {
            this.friend.Add(iID);
            deleteVisit(iID);
        }
        public void deleteVisit(string iID)
        {
            this.IDVisit.Remove(iID);
        }
        public void sendVisit(string iID)
        {
            User temp=new User(iID);
            temp.IDVisit.Add(this.ID);
            temp.writeFile();
        }
        public void reactWithUser(string choice, string iID)
        {
            bool isVisit=support.isIn(iID,IDVisit);
            bool isFriend=support.isIn(iID,friend);
            if (choice=="1")
                if (isVisit==true)
                    acceptVisit(iID);
                else
                    if (isFriend==false)
                        sendVisit(iID);
                    else
                        deleteFriend(iID);
            else if (choice=="2")
                viewWall(iID);
            else if (choice=="3")  
                findChat(iID);
            else if (choice=="4")
                deleteVisit(iID);
            writeFile();
        }
        public void findChat(string iID)
        {
            string idChat=findIDChat(iID);
            if (idChat!="")
            {
                mainChat=new Chat(idChat);
                mainChat.display();
            }
            else
            {
                mainChat=new Chat(this.ID,iID);
                mainChat.display();
            }
        }
        public string findIDChat(string iID)
        {
            string t1=ID+iID;
            string t2=iID+ID;
            if (support.checkExist($"data/chat/{t1}")==true)
                return t1;
            else if (support.checkExist($"data/chat/{t2}")==true)
            {
                return t2;
            }
            else
                return "";
        }
        public void deleteFriend(string iID){
            friend.Remove(iID);
            writeFile();
        }
        public void viewRequestion()
        {
            Console.WriteLine("{0,-10} | {1,-30}","ID","Ten");
            foreach(string i in IDVisit)
            {
                try{
                    string temp=File.ReadAllLines($"data/user/{i}")[3];
                    Console.WriteLine("{0,-10} | {1,-30}",i,temp);
                    }
                catch{}
            }
        }
    }
}