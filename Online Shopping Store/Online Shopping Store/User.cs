using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Online_Shopping_Store
{
    public class User
    {
        public string email, un, pw,cc, address, phone, gender, bd;
        public User()
        {

        }
        public User(string[] s)
        {
            email = s[0];
            un = s[1];
            pw = s[2];
            cc = s[3];
            address = s[4];
            phone = s[5];
            gender = s[6];
            bd = s[7];
        }
        public Dictionary<string, User> ReadFromFile()
        {
            Dictionary<string, User> d = new Dictionary<string, User>();
            if(File.Exists("User_Data.txt"))
            {
                StreamReader sr = new StreamReader("User_Data.txt");
                while (!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(';');
                    User u = new User(s);
                    d[s[0]] = u;
                }
                sr.Close();
            }
            return d;
        }
       
        public void Update(Dictionary<string,User>d)
        {
            StreamWriter sr = new StreamWriter("User_Data.txt");
            sr.Flush();
            sr.Close();

            foreach (KeyValuePair<string, User> u in d)
            {
                u.Value.WriteToFile();
            }
        }
        public void Delete()
        {
            Dictionary<string, User> d=ReadFromFile();
            d.Remove(email);
            Update(d);
        }
        public void WriteToFile()
        {
            StreamWriter sr = new StreamWriter("User_Data.txt", true);
            sr.WriteLine(email + ";" + un + ";" + pw + ";" + cc + ";" + address + ";" + phone + ";" + gender + ";" + bd);
            sr.Close();
        }
    }
}
