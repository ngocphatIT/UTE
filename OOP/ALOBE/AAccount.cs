using System;
using System.Collections.Generic;
namespace alobe
{
    public class Account
    {
        public string name;
        public string ID="";
        public DateTime birthday;
        public bool stateAccount=true;
        public bool stateLogin=false;
        public Information info=new Information();
        public List<string> IDPost=new List<string>();
        public List<string> IDFollow=new List<string>();
        public List<string> IDVisit=new List<string>();
        public List<string> IDChat=new List<string>();
        public Support support=new Support();
        public virtual void login(string username,string password){}
        public virtual void logout(){}
        public virtual void register(string iuser, string ipassword, string iname,Question iquestion){}

    }
}