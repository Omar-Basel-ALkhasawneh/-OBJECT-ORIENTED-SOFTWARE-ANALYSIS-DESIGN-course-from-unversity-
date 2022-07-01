using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
internal class post
{
    private string Username;
    private string Content;
    private string Category;

    public post(string username, string content, string category)
    {
        Username = username;
        Content = content;
        Category = category;
    }

    public string GetUsername()
    {
        return Username;
    }
    public string GetContent()
    {
        return Content;

    }
    public string GetCategory()
    {
        return Category;
    }
}

[Serializable]
internal class message
{
    private string sender;
    private int number_recivers;
    private List<string> recievers ;
    private string text;
    string subject;
    string priority;  
    public void SetMessage(string sen, int number,List<string> rec, string content, string subject, string pri)
    {
        this.text = content;
        this.sender = sen;
        this.subject = subject;
        this.priority = pri;
        this.number_recivers = number;
        this.recievers=new List<string>();
        for(int i=0;i<number;i++)
        {
            this.recievers.Add(rec[i]);
        }
    }
    // omar AL-khasawneh
    public int getnumber()
    { return number_recivers; }
    public List<string> Getreciever()
    {
        return this.recievers;
    }
    public string GetText()
    {
        return text;
    }
    public string GetSubject()
    {
        return subject;
    }
    public string GetPriority()
    {
        return priority;
    }
    public string GetSendername()
    {
        return sender;
    }
}

[Serializable]
internal class report
{
    private string reporter;
    private string reported;
   

    public report(string reporter, string reported)
    {
        this.reporter = reporter;
        this.reported = reported;
       
    }

    public void print() //omar AL-khasawneh 
    {
        Console.WriteLine("reporter :" + this.reporter + "  reported: " + this.reported);
    }
    public string GetReported() // Omar AL-khasawneh 
    {
        return reported;
    }

}

[Serializable]
internal class user 
{
    
    public string Username;

    public string Password;
    public string Status;
    public string FirstName;
    public string LastName;
    public string Location;
    public int Age;

    public List<string> Friends;

    public user(string username, string password, string status, string firstName, string lastName, string location, int age, List<string> friends)
    {
        Username = username;
        Password = password;
        Status = status;
        FirstName = firstName;
        LastName = lastName;
        Location = location;
        Age = age;
        Friends = friends;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (obj.GetType() != GetType())
            return false;
        user u = (user)obj;
        if (u.Username == Username && u.LastName == LastName && u.FirstName == FirstName && u.Status == Status && u.Location == Location && u.Age == Age)
            return true;

        return false;


    }
    public void printAll()
    {
        Console.WriteLine("Username :" + Username + " password :" + Password + " Status:" + Status + " FirstName:" + FirstName + " LastName :" + LastName + " Location :" + Location + " Age :" + Age);
        Console.WriteLine("name of friends ");
        for (int i = 0; i < Friends.Count; i++)
        {
            Console.WriteLine(Friends[i]);
        }
    }

    public string GetUsername()
    {
        return Username;
    }

    public string Getstatus()
    {
        return Status;
    }
    public string GetPassword()
    {
        return Password;
    }

    public void SetStatus(string Status)
    {
        this.Status = Status;
    }

    public void PostNewContent()// OMAR AL- KHASAWNEH
    {
        Console.WriteLine("please enter the content of post");
        string content = Console.ReadLine();
        Console.WriteLine("please enter the category");
        string caregory = Console.ReadLine();
        post p = new post(Username, content, caregory);
        database.save(p);
        Console.WriteLine("the post save in database");
    }

    public void SendMessage()// OMAR AL- KHASAWNEH
    {
        List<user> acitveUser = new List<user>();
        for (int i = 0; i < database.users.Count; i++)
        {
            if (database.users[i].Getstatus() == "Active")
            {
                acitveUser.Add(database.users[i]);
                Console.WriteLine(database.users[i].GetUsername());
            }

        }
        Console.WriteLine("enter the number of users who want to send massege");
        int number_user=Convert.ToInt32(Console.ReadLine());
        List<string>users = new List<string>();
        for(int i=0;i<number_user;i++)
        {
            Console.WriteLine("enter the receiver name");
            string NameReceiver = Console.ReadLine();
            users.Add(NameReceiver);
        }
        
        Console.WriteLine("enter subject ");
        string subjectMassege = Console.ReadLine();
        Console.WriteLine("enter priority ");
        string priority = Console.ReadLine();
        Console.WriteLine("please enter the content massege");
        string content = Console.ReadLine();
        message m = new message();
        m.SetMessage(Username, number_user,users, content, subjectMassege, priority);
        database.save(m);
    }

    public void ViewAllMyPosts() // OMAR AL-SALEH
    {
        // int numberofposts = 0;
        for (int i = 0; i < database.posts.Count; i++) // we must follow life description (format of post |category )
        {
            if (database.posts[i].GetUsername() == Username)
            {
                Console.WriteLine(database.posts[i].GetContent() + "| " + database.posts[i].GetCategory());
            }
        }

    }

    public void ViewAllMyReceivedMessages() // OMAR AL-SALEH
    {
        List<message> messages = new List<message>();
        for (int i = 0; i < database.messages.Count; i++)
        {
            List<string> messages2 = database.messages[i].Getreciever();
            
            for (int j = 0; j < database.messages[i].getnumber(); j++)
            {
                if (Username == messages2[j])
                {
                    messages.Add(database.messages[i]);
                    break;
                }
            }
        }

        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].GetPriority() == "high")
            {
                Console.WriteLine(messages[i].GetPriority() + "|" + messages[i].GetSendername() + "|" + messages[i].GetSubject() + "|" + messages[i].GetText());
            }
        }

        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].GetPriority() == "low")
            {
                Console.WriteLine(messages[i].GetPriority() + "|" + messages[i].GetSendername() + "|" + messages[i].GetSubject() + "|" + messages[i].GetText());
            }
        }
    }

    public void ViewAllMyLastUpdatedWall()// OMAR AL-SALEH
    {
        for (int i = 0; i < Friends.Count; i++)
        {
            for (int j = 0; j < database.posts.Count; j++)
            {
                if (database.posts[j].GetUsername() == Friends[i])
                {
                    Console.WriteLine(Friends[i] + "|" + database.posts[j].GetContent() + "|" + database.posts[j].GetCategory());
                   
                }
            }
        }
    }

    public void FilterMyWall()//  HASHEM AL-RADAIDEH
    {
        ViewAllMyLastUpdatedWall();
        Console.WriteLine("enter the category");
        string category = Console.ReadLine();

        for (int i = 0; i < Friends.Count; i++)
        {
            for (int j = 0; j < database.posts.Count; j++)
            {
                if (database.posts[j].GetUsername() == Friends[i] && database.posts[j].GetCategory() == category)
                {
                    Console.WriteLine(Friends[i] + "|" + database.posts[j].GetContent() + "|" + database.posts[j].GetCategory());
                }
            }
        }
    }

    public void SendReportToAdministrator()//HASHEM AL- RADAIDEH
    {

        for (int i = 0; i < database.users.Count; i++)
        {
            if (database.users[i].Status == "Active")
            {
                Console.WriteLine("Username: " + database.users[i].GetUsername());
            }
        }

        Console.Write("enter the name of the user to report: ");
        var reported = Console.ReadLine();

        report r = new report(this.Username, reported);
        database.save(r);
    }
}


[Serializable]
internal class administrator 
{
    private string Username = "admin";
    private int Password = 0;
    public void RegisterNewUserAccount()
    {
        Console.WriteLine("please enter username:");
        string username = Console.ReadLine();
        Console.WriteLine("please enter password:");

        string password = Console.ReadLine();
        Console.WriteLine("please enter status:");
        string status = Console.ReadLine();
        Console.WriteLine("please enter firstname:");
        string firstname = Console.ReadLine();
        Console.WriteLine("please enter lastname:");
        string lastname = Console.ReadLine();

        Console.WriteLine("please enter location:");
        string location = Console.ReadLine();

        Console.WriteLine("please enter age:");
        string age1 = Console.ReadLine();
        int age = Convert.ToInt32(age1);

        Console.WriteLine("please enter the number of friends ");
        string n1 = Console.ReadLine();
        int num = Convert.ToInt32(n1);
        Console.WriteLine("please enter friends of user ");
        List<string> friends = new List<string>();
        for (int i = 0; i < num; i++)
        {

            string input1 = Console.ReadLine();
            friends.Add(input1);

        }
        user u = new user(username, password, status, firstname, lastname, location, age, friends);
        for (int i = 0; i < database.users.Count; i++) //check if user is found 
        {
            if (Equals(database.users[i], u))
            {
                Console.WriteLine("the user is exist ");
                return;
            }
        }
        database.save(u);
        Console.WriteLine("user saved in Database ");
    }

    public void ViewAllUserAccounts()// OMAR AL-SALEH
    {
        Console.WriteLine("Users Account");
        for (int i = 0; i < database.users.Count; i++)
        {
            database.users[i].printAll();
        }
    }

    public void SuspendUserAccount()// OMAR AL- KHASAWNEH
    {
        // step 1 print all report 
        for (int i = 0; i < database.reports.Count; i++)
        {
            database.reports[i].print();
        }
        // step 2 fliter to user have 2 report or more 
        List<string> reportedUsers = new List<string>();
        Console.WriteLine("the reported User (have 2 or more reported )");
        for (int i = 0; i < database.users.Count; i++)
        {
            int numberOfReport = 0;
            for (int j = 0; j < database.reports.Count; j++)
            {
                if (database.reports[j].GetReported() == database.users[i].GetUsername())
                {
                    numberOfReport++;
                }

                if (numberOfReport >= 2)
                {
                    Console.WriteLine(database.users[i].GetUsername()); //this my erorr
                    reportedUsers.Add(database.users[i].GetUsername());
                    break;
                }
            }
        }


        // step 3  ask username for user who want to suspend 
        Console.WriteLine("enter the username , for the user you want to suspend ");
        string userNAME = Console.ReadLine();
        // check if user is in the list
        bool Flag = false;
        for (int i = 0; i < reportedUsers.Count; i++)
        {
            if (userNAME == reportedUsers[i]) Flag = true;
        }
        if (Flag == false)
        {
            Console.WriteLine("the user don't have enough report ");
            return;
        }
        // step 4 set uer to suspend
        for (int i = 0; i < database.users.Count; i++)
        {
            if (userNAME == database.users[i].GetUsername())
            {
                database.users[i].SetStatus("Suspended");
                break;
            }
        }
        // step 5 delet from report list 
        for (int i = 0; i < database.reports.Count; i++)
        {
            if (userNAME == database.reports[i].GetReported())
            {
                database.remove(database.reports[i]);
            }
        }

        Console.WriteLine("the user suspended");

    }
    public void ActivateUserAccount()// OMAR AL- KHASAWNEH
    {
        // step 1 display all users suspend
        List<user> suspendUser = new List<user>();
        for (int i = 0; i < database.users.Count; i++)
        {
            if (database.users[i].Getstatus() == "Suspended")
            {
                suspendUser.Add(database.users[i]);
                Console.WriteLine(database.users[i].GetUsername());
            }
        }

        if (suspendUser.Count == 0)
        {
            Console.WriteLine("we don't have any suspended user");
            return;
        }

        // step 2 enter the username and step 3 active username 
        Console.WriteLine("enter the username you want to Active ");
        string useractive = Console.ReadLine();
        for (int i = 0; i < database.users.Count; i++)
        {
            if (useractive == database.users[i].GetUsername())
            {
                database.users[i].SetStatus("Active");
                Console.WriteLine("the operation will don");
            }
        }
    }
}

[Serializable]
internal class Number// OMAR AL-SALEH
{
    public static int NumberOfUsers = 0;
    public static int NumberOfPosts = 0;
    public static int NumberOfMessages = 0;
    public static int NumberOfReports = 0;
}

[Serializable]
internal class database// OMAR AL-SALEH
{
    public static List<user> users = new List<user>();
    public static List<post> posts = new List<post>();
    public static List<message> messages = new List<message>();
    public static List<report> reports = new List<report>();

    public static void decode()
    { //do not edit any thing in this class befor tell (omar alsaleh )
        BinaryFormatter bf = new BinaryFormatter();
        FileStream u = new FileStream("NumberOfUsers.txt", FileMode.Open, FileAccess.Read);
        Number.NumberOfUsers = (int)bf.Deserialize(u);
        u.Close();
        ////////////////////////////////////////////////////////////////////////////////////
        FileStream p = new FileStream("NumberOfPosts.txt", FileMode.Open, FileAccess.Read);
        Number.NumberOfPosts = (int)bf.Deserialize(p);
        p.Close();
        //////////////////////////////////////////////////////////////////////////////////////
        FileStream m = new FileStream("NumberOfMessages.txt", FileMode.Open, FileAccess.Read);
        Number.NumberOfMessages = (int)bf.Deserialize(m);
        m.Close();
        //////////////////////////////////////////////////////////////////////////////////////////
        FileStream r = new FileStream("NumberOfReports.txt", FileMode.Open, FileAccess.Read);
        Number.NumberOfReports = (int)bf.Deserialize(r);
        r.Close();

        //////////////////////////////////////////////////////////////////////////////////////
        FileStream fff = new FileStream("users.txt", FileMode.Open, FileAccess.Read);

        user c;
        for (int i = 0; i < Number.NumberOfUsers; i++)
        {
            c = (user)bf.Deserialize(fff);
            users.Add(c);
        }
        fff.Close();
        ////////////////////////////////////////////////////////////////////////////////////////
        FileStream ffff = new FileStream("posts.txt", FileMode.Open, FileAccess.Read);
        post g;
        for (int i = 0; i < Number.NumberOfPosts; i++)
        {
            g = (post)bf.Deserialize(ffff);
            posts.Add(g);
        }
        ffff.Close();
        ////////////////////////////////////////////////////////////////////////////////////////
        FileStream fffff = new FileStream("messages.txt", FileMode.Open, FileAccess.Read);
        message mm;
        for (int i = 0; i < Number.NumberOfMessages; i++)
        {
            mm = (message)bf.Deserialize(fffff);
            messages.Add(mm);
        }
        fffff.Close();
        //////////////////////////////////////////////////////////////////////////////////////////////
        FileStream ffffff = new FileStream("reports.txt", FileMode.Open, FileAccess.Read);
        report re;
        for (int i = 0; i < Number.NumberOfReports; i++)
        {
            re = (report)bf.Deserialize(ffffff);
            reports.Add(re);
        }
        ffffff.Close();
    }

    public static void save(user u)
    {
        users.Add(u);
        Number.NumberOfUsers++;
    }

    public static void save(post p)
    {
        posts.Add(p);
        Number.NumberOfPosts++;

    }

    public static void save(message m)
    {
        messages.Add(m);
        Number.NumberOfMessages++;
    }

    public static void save(report r)
    {
        reports.Add(r);
        Number.NumberOfReports++;
    }

    public static void remove(user u)
    {
        bool b = users.Remove(u);
        if (b)
            Number.NumberOfUsers--;
    }

    public static void remove(post p)
    {
        bool b = posts.Remove(p);
        if (b)
            Number.NumberOfPosts--;
    }

    public static void remove(message m)
    {
        bool b = messages.Remove(m);
        if (b)
            Number.NumberOfMessages--;
    }

    public static void remove(report r)
    {
        bool b = reports.Remove(r);
        if (b)
            Number.NumberOfReports--;
    }

    public static void finish()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream f = new FileStream("users.txt", FileMode.Create, FileAccess.Write);
        for (int i = 0; i < Number.NumberOfUsers; i++)
        {
            bf.Serialize(f, users[i]);
        }
        f.Close();
        //////////////////////////////////////////////////////////////////////////////////
        FileStream uu = new FileStream("NumberOfUsers.txt", FileMode.Create, FileAccess.Write);
        bf.Serialize(uu, Number.NumberOfUsers);
        uu.Close();
        //////////////////////////////////////////////////////////////////////////////////
        FileStream ff = new FileStream("posts.txt", FileMode.Create, FileAccess.Write);
        for (int i = 0; i < Number.NumberOfPosts; i++)
        {
            bf.Serialize(ff, posts[i]);
        }
        ff.Close();
        ///////////////////////////////////////////////////////////////////////////////////
        FileStream pp = new FileStream("NumberOfPosts.txt", FileMode.Create, FileAccess.Write);
        bf.Serialize(pp, Number.NumberOfPosts);
        pp.Close();
        ////////////////////////////////////////////////////////////////////////////////////
        FileStream fff = new FileStream("messages.txt", FileMode.Create, FileAccess.Write);
        for (int i = 0; i < Number.NumberOfMessages; i++)
        {
            bf.Serialize(fff, messages[i]);
        }
        fff.Close();
        ///////////////////////////////////////////////////////////////////////////////////////////
        FileStream mm = new FileStream("NumberOfMessages.txt", FileMode.Create, FileAccess.Write);
        bf.Serialize(mm, Number.NumberOfMessages);
        mm.Close();
        /////////////////////////////////////////////////////////////////////////////////////////////
        FileStream ffff = new FileStream("reports.txt", FileMode.Create, FileAccess.Write);
        for (int i = 0; i < Number.NumberOfReports; i++)
        {
            bf.Serialize(ffff, reports[i]);
        }
        ffff.Close();
        ///////////////////////////////////////////////////////////////////////////////////////////////
        FileStream rr = new FileStream("NumberOfReports.txt", FileMode.Create, FileAccess.Write);
        bf.Serialize(rr, Number.NumberOfReports);
        rr.Close();
    }
}

namespace Core
{
    public class Program
    {
        public static void Main(string[] args) //HASHEM AL-RADAIDEH
        {

            database.decode();
            while (true)
            {
                Console.WriteLine("[1] Login as administrator");
                Console.WriteLine("[2] Login as user");
                Console.WriteLine("[3] Exit");

                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var admin = GetLogin();

                        if (admin != null)
                        {
                            AdminLoop((administrator)admin);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid username or password! \n");
                        }
                        break;

                    case "2":
                        var user = GetLogin();

                        if (user != null)
                        {
                            UserLoop((user)user);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid username or password || Or you are Suspended\n");
                        }
                        break;

                    case "3":
                        Console.WriteLine("\nExiting...");
                        database.finish();
                        return;

                    default:
                        Console.WriteLine("\nInvalid choice");
                        break;
                }
            }
        }

        /// <summary>
        /// Part of the main loop for the administrator.
        /// </summary>
        /// <param name="admin">The administrator.</param>
        private static void AdminLoop(administrator admin)
        {
            while (true)
            {
                Console.WriteLine("\nWelcome back admin, What would you like to do?");
                Console.WriteLine("1. Register new user account");
                Console.WriteLine("2. View all user accounts");
                Console.WriteLine("3. Suspend user account");
                Console.WriteLine("4. Activate user account");
                Console.WriteLine("5. Exit");

                Console.Write("\nEnter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        admin.RegisterNewUserAccount();
                        break;

                    case "2":
                        admin.ViewAllUserAccounts();
                        break;

                    case "3":
                        admin.SuspendUserAccount();
                        break;

                    case "4":
                        admin.ActivateUserAccount();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        /// <summary>
        /// Part of the main loop for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        private static void UserLoop(user user)
        {
            while (true)
            {
                Console.WriteLine($"\nWelcome back {user.Username}, What would you like to do?");
                Console.WriteLine("1. Post new content");
                Console.WriteLine("2. Send message");
                Console.WriteLine("3. View all my posts");
                Console.WriteLine("4. View all my received messages");
                Console.WriteLine("5. View all my last updated wall");
                Console.WriteLine("6. Filter my wall");
                Console.WriteLine("7. Send report to administrator");
                Console.WriteLine("8. Exit");

                Console.Write("\nEnter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        user.PostNewContent();
                        break;

                    case "2":
                        user.SendMessage();
                        break;

                    case "3":
                        user.ViewAllMyPosts();
                        break;

                    case "4":
                        user.ViewAllMyReceivedMessages();
                        break;

                    case "5":
                        user.ViewAllMyLastUpdatedWall();
                        break;

                    case "6":
                        user.FilterMyWall();
                        break;

                    case "7":
                        user.SendReportToAdministrator();
                        break;

                    case "8":
                        return;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <returns>The login.</returns>
        private static object GetLogin()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            if (username == "admin" && password == "0")
            {
                return new administrator();
            }

            for(int i=0;i<database.users.Count;i++)
            {
                if (database.users[i].Getstatus()== "Active")
                {
                    if (database.users[i].GetUsername() == username && database.users[i].GetPassword() == password)
                    {
                        return database.users[i];
                    }
                }
            }

            return null;
        }
    }
}

