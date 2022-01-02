using System.Collections.Generic;
namespace alobe
{
    class Program
    {
        static Support support= new Support();
        static List<User> UserList= new List<User>();
        static string[] IDList;
        static void Main(string[] args)
        {
            Screen sc=new Screen(new User(),1);
            while (sc.level!=0)
            {
                if (sc.level==1)
                {
                    update();
                    sc.mainUser.userList=UserList;
                }
                sc.display();   
            }
        }
        static void update()
        {
            IDList=support.readFile("UserIDList.dat");
            foreach(string i in IDList)
            {
                try
                {
                    User temp= new User(ID:i);
                    temp.readFile();
                    UserList.Add(temp);
                }
                catch{}
            }
        }  
    }
}
