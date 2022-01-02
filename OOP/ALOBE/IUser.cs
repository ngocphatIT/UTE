namespace alobe
{
    public interface IUser
    {
        public void resetPassword(string password);
        public void changePassword(string password);
        public void myWall();
        public void findChat(string iID);
        public string findIDChat(string iID);
        public void viewRequestion();

        

    }
}