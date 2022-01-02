using System;
namespace alobe
{
    public class Information
    {
        public string name;
        public string hobby;
        public string gender;
        public string phone; 
        public string address;
        public Information(){}
        ~Information(){}
        public void display(){
            Console.WriteLine("{0,-20} : {1,-100}","Ho va ten",name);
            Console.WriteLine("{0,-20} : {1,-100}","So thich",hobby);
            Console.WriteLine("{0,-20} : {1,-100}","So dien thoai",phone);
            Console.WriteLine("{0,-20} : {1,-100}","Dia chi",address);
            Console.WriteLine("{0,-20} : {1,-100}","Gioi tinh",gender);
        }
        public string convertToString()
        {
            return $"{name}"+"\n"+$"{hobby}"+"\n"+$"{phone}"+"\n"+$"{address}"+"\n"+$"{gender}";
        }
    }
}