namespace alobe
{
    public interface IOperation
    {
         public void update(int choice, string data);
         public void lockAccount();
         public void post();
         public void reactWithUser(string choice, string iID);
    }
}