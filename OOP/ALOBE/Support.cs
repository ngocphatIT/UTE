using System.IO;
using System.Collections.Generic;
using System;
namespace alobe
{
    public class Support
    {
        public Support(){}
        ~Support(){}
        public void appendFile(string fileName, string data)
        {
            File.AppendAllText(fileName,"\n"+data);
        }
        public string[] readFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
        public void writeFile(string fileName, string[] data)
        {
            File.WriteAllText(fileName,data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                appendFile(fileName,data[i]);
            }
        }
        public void writeFile(string fileName, string data)
        {
            File.WriteAllText(fileName,data);
        }
        public bool checkExist(string fileName)
        {
            return File.Exists(fileName);
        }
        public bool isIn(string input, List<string> data){
            foreach (string i in data)
            {
                if (i==input) return true;
            }
            return false;
        }
        public void print(int n, string type)
        {
            for(int i=0;i<n;i++) Console.Write(type);
            Console.WriteLine();
        }
    }
}