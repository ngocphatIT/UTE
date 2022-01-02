using System;
namespace alobe
{
    public class Screen: IView
    {
        public User mainUser;
        public int level;
        public Screen(){}
        ~Screen(){}
        public Screen(User mainUser, int level){
            this.mainUser = mainUser;
            this.level = level;
        }
        public void display(){
            if (level==1)
                mainScreen();
            else if (level==2)
                userScreen();
        }
        public void mainScreen()
        {
            Console.WriteLine("\t\t\t\tALOBE");
            Console.WriteLine("\t\t\t[1]. Dang nhap");
            Console.WriteLine("\t\t\t[2]. Dang ki");
            Console.WriteLine("\t\t\t[3]. Quen mat khau");
            Console.WriteLine("\t\t\t[0]. Thoat");
            string choice=Console.ReadLine();
            Console.Clear();
            switch(choice)
            {
                case "1":
                    loginScreen();
                    if (mainUser.stateLogin==false)
                        level=1;
                    level=2;
                    break;
                case "2":
                    registerScreen();
                    level=1;
                    break;
                case "3":
                    resetPasswordScreen();
                    level=1;
                    break;
            }
        }
        public void loginScreen()
        {
            int i=0;
            string utemp,ptemp,choice;
            while (mainUser.stateLogin==false)
            {
                if (i!=0)
                {
                    Console.WriteLine("Tai khoan hoac mat khau khong chinh xac");
                    Console.WriteLine("[1]. Tiep tuc dang nhap");
                    Console.WriteLine("[0]. Thoat");
                    choice=Console.ReadLine();
                    Console.Clear();
                    if (choice=="0")
                        break;
                }
                Console.Write("Nhap tai khoan: ");
                utemp=Console.ReadLine();
                Console.Write("Nhap mat khau: ");
                ptemp=Console.ReadLine();
                mainUser.login(utemp,ptemp);
                i++;
            }
            Console.WriteLine("Dang nhap thanh cong");
        }
        public void registerScreen()
        {
            int i=0;
            string utemp,ptemp,qtemp,atemp,ntemp;
            do
            {
                if (i!=0)
                    Console.WriteLine("Tai khoan da duoc su dung!");
                Console.Write("Nhap tai khoan: ");
                utemp=Console.ReadLine();
                Console.Write("Nhap mat khau: ");
                ptemp=Console.ReadLine();
                Console.Write("Nhap ten hien thi: ");
                ntemp=Console.ReadLine();
                Console.Write("Nhap cau hoi xac minh: ");
                qtemp=Console.ReadLine();
                Console.Write("Nhap cau tra loi: ");
                atemp=Console.ReadLine();
                i++;
            }
            while (mainUser.checkUser(utemp)==false);
            Console.WriteLine("[1]. Tiep tuc");
            Console.WriteLine("[0]. Thoat");
            string temp=Console.ReadLine();
            Console.Clear();
            if (temp=="1")
            {
                mainUser.register(iuser:utemp,ipassword:ptemp,iname:ntemp,iquestion:new Question(qtemp,atemp));
            }
        }
        public void resetPasswordScreen()
        {
            string utemp,ptemp,atemp;
            do
            {
                Console.Write("Nhap tai khoan: ");
                utemp=Console.ReadLine();
            }
            while (mainUser.checkUser(utemp)==true);
            foreach(User user in mainUser.userList)
            {
                if (user.user==utemp)
                    mainUser.ID=user.ID;
            }
            mainUser.readFile();
            Console.Write("Cau hoi xac minh: ");
            Console.WriteLine(mainUser.question.question);
            Console.WriteLine("Nhap cau tra loi: ");
            atemp=Console.ReadLine();
            while (mainUser.question.check(atemp)==false)
            {
                Console.Clear();
                Console.WriteLine("Cau tra loi chua dung! Nhap ^ de thoat");
                Console.WriteLine(mainUser.question.question);
                Console.WriteLine("Nhap cau tra loi: ");
                atemp=Console.ReadLine(); 
                if (atemp=="^")
                    return;
            }
            Console.Write("Nhap mat khau moi: ");
            ptemp=Console.ReadLine();
            mainUser.resetPassword(ptemp);
            Console.Clear();
            Console.WriteLine("Cap nhat thanh cong");
        }
        public void userScreen()
        {
            Console.WriteLine("\t\t\t\tALOBE");
            Console.WriteLine("\t\t\t[1]. Doi mat khau");
            Console.WriteLine("\t\t\t[2]. Khoa/Mo tai khoan");
            Console.WriteLine("\t\t\t[3]. Xem loi moi ket ban");
            Console.WriteLine("\t\t\t[4]. Tim kiem nguoi dung");
            Console.WriteLine("\t\t\t[5]. Dang bai viet");
            Console.WriteLine("\t\t\t[6]. Xem trang ca nhan");
            Console.WriteLine("\t\t\t[7]. Chinh sua thong tin");
            Console.WriteLine("\t\t\t[0]. Dang xuat");
            int choice=Int32.Parse(Console.ReadLine());
            Console.Clear();
            switch(choice){
                case 1:
                    changePasswordScreen();
                    Console.WriteLine("Thay doi mat khau thanh cong");
                    break;
                case 2:
                    mainUser.lockAccount();
                    break;
                case 3:
                    mainUser.viewRequestion();
                    reactWithUserScreen(iID:"",choose:0);
                    break;
                case 4:
                    findUserScreen();
                    break;
                case 5:
                    mainUser.post();
                    break;
                case 6:
                    mainUser.myWall();
                    break;
                case 7:
                    updateScreen();
                    break;
                case 0:
                    mainUser.writeFile();
                    break;
            }
            if (choice!=0)
                level=2;
            else
                level=1;
        }
        public void changePasswordScreen()
        {
            string temp="";
            Console.Write("Nhap mat khau cu (0 de thoat): ");
            temp=Console.ReadLine();
            if (temp=="0")
                return;
            while (temp!=mainUser.password)
            {
                Console.Clear();
                Console.WriteLine("Mat khau sai!");
                Console.Write("Nhap mat khau cu (0 de thoat): ");
                temp=Console.ReadLine();
                if (temp=="0")
                     return;
            }
            Console.Write("Nhap mat khau moi: ");
            string password=Console.ReadLine();
            mainUser.changePassword(password);
            Console.Clear();
        }
        public void reactWithUserScreen(string iID, int choose=1)
        {
            if (choose==0)
            {
                iID=choiceUserScreen();
            }
            if (iID=="")
                return;
            bool isVisit=mainUser.support.isIn(iID,mainUser.IDVisit);
            bool isFriend=mainUser.support.isIn(iID,mainUser.friend);
            if (isVisit==true)
                Console.WriteLine("\t\t\t[1]. Chap nhan loi moi");
            else
                if (isFriend==false)
                    Console.WriteLine("\t\t\t[1]. Gui loi moi ket ban");
                else
                    Console.WriteLine("\t\t\t[1]. Huy ket ban");
            Console.WriteLine("\t\t\t[2]. Xem trang ca nhan");
            Console.WriteLine("\t\t\t[3]. Nhan tin");
            if (isVisit==true) 
                Console.WriteLine("\t\t\t[4]. Xoa loi moi");
            Console.WriteLine("\t\t\t[0]. Thoat");
            string choice=Console.ReadLine();
            Console.Clear();
            mainUser.reactWithUser(choice:choice,iID:iID);
            if (choice=="2")
                reactWithUserScreen(iID);
        }
        public void findUserScreen(){
            bool isFind;
            string utemp;
            Console.Write("Nhap ID/Ten de tim kiem: ");
            string temp=Console.ReadLine();
            isFind=mainUser.findUser(temp);
            if (isFind==true)
            {
                utemp=choiceUserScreen();
                reactWithUserScreen(utemp);
            }
            else
            {
                Console.WriteLine("Khong tim thay");
            }
        }
        public string choiceUserScreen()
        {
            string run;
            while (true)
            {
                Console.WriteLine("Nhap <ID> de tuong tac");
                Console.WriteLine("[0]. Thoat");
                run=Console.ReadLine();
                if (Int32.Parse(run)!=0)
                    if (mainUser.support.checkExist($"data/user/{run}")==true)
                        return run;
                    else
                        Console.WriteLine("Khong tim thay ID");
                else
                {
                    Console.Clear();
                    return "";
                }
            }
        }
        public void updateScreen()
        {
            
            int run=1;
            string temp="";
            while (run!=0)
            {
                Console.WriteLine("\t\t\t[1]. Chinh sua ten hien thi");
                Console.WriteLine("\t\t\t[2]. Chinh sua sinh nhat");
                Console.WriteLine("\t\t\t[3]. Chinh sua ho va ten");
                Console.WriteLine("\t\t\t[4]. Chinh sua dia chi");
                Console.WriteLine("\t\t\t[5]. Chinh sua so dien thoai");
                Console.WriteLine("\t\t\t[6]. Chinh sua so thich");
                Console.WriteLine("\t\t\t[7]. Chinh sua gioi tinh");
                Console.WriteLine("\t\t\t[0]. Quay lai");
                run=Int32.Parse(Console.ReadLine());
                Console.Clear();
                switch(run)
                {
                    case 1:
                        Console.Write("Nhap ten hien thi: ");
                        temp=Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Nhap sinh nhat (dd mm yyyy): ");
                        temp=Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Nhap ho va ten: ");
                        temp=Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Nhap dia chi: ");
                        temp=Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Nhap so dien thoai: ");
                        temp=Console.ReadLine();
                        break;
                    case 6:
                        Console.Write("Nhap so thich: ");
                        temp=Console.ReadLine();
                        break;
                    case 7:
                        Console.Write("Nhap dia chi: ");
                        temp=Console.ReadLine();
                        break;

                }
                mainUser.update(run,temp);
            }
        }
    }
}