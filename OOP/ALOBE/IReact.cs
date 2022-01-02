namespace alobe
{
    public interface IReact
    {
         public bool findUser(string iID);
         public void viewWall(string iID);
         public void sendVisit(string iID);
         public void acceptVisit(string iID);
         public void deleteVisit(string iID);
         public void deleteFriend(string iID);
    }
}