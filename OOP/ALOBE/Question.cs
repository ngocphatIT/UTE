using System;
namespace alobe
{
    public class Question
    {
        public string question;
        public string answer;
        ~Question(){}
        public Question( string question, string answer)
        {
            this.question = question;
            this.answer = answer;
        }
        public bool check(string answer)
        {
            return  this.answer==answer;
        }
    }
}