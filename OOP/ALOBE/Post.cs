using System;
using System.IO;
namespace alobe
{
    public class Post: Account, IData
    {
        public string content;
        public Post(){}
        public Post(string ID)
        {
            this.ID = ID;
        }
        ~Post(){}
        public string createPost()
        {
            Console.Write("Nhap noi dung bai viet (Nhap -1 de thoat): ");
            this.content=Console.ReadLine();
            if (this.content=="-1")
                return "-1";
            this.birthday=DateTime.Today;
            return createID();
        }
        public string createID()
        {

            string temp;
            Random rand = new Random();
            int num;
            do
            {
                temp="";
                for (int i = 0; i <10; i++)
                {
                    num=rand.Next(0,9);
                    temp+=num.ToString();
                }
            }
            while(support.checkExist($"data/post/{temp}")==true);
            this.ID=temp;
            return temp;
            
        }
        public void writeFile()
        {
            File.WriteAllText($"data/post/{ID}",$"{ID}"+"\n"+$"{content}"+"\n"+$"{birthday.Day}"+"\n"+$"{birthday.Month}"+"\n"+$"{birthday.Year}");
        }
        public void readFile(){
            string[] temp=File.ReadAllLines($"data/post/{ID}");
            this.ID=temp[0];
            this.content=temp[1];
            birthday=new DateTime(Int32.Parse(temp[4]),Int32.Parse(temp[3]),Int32.Parse(temp[2]));
        }

    }
}