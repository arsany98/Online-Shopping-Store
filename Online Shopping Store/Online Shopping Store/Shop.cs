using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Online_Shopping_Store
{
    public class Shop
    {
        public string email, name, pw, cc, address, phone;
        public Shop()
        {

        }
        public Shop(string[] s)
        {
            email = s[0];
            name = s[1];
            pw = s[2];
            address = s[3];
            phone = s[4];
        }
        public Dictionary<string, Shop> ReadFromFile()
        {
            Dictionary<string, Shop> d = new Dictionary<string, Shop>();
            if (File.Exists("Shop_Data.txt"))
            {
                StreamReader sr = new StreamReader("Shop_Data.txt");
                while (!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(';');
                    Shop a = new Shop(s);
                    d[s[0]] = a;
                }
                sr.Close();
            }
            return d;
        }
        public void Update(Dictionary<string, Shop> d)
        {
            StreamWriter sr = new StreamWriter("Shop_Data.txt");
            sr.Flush();
            sr.Close();

            foreach (KeyValuePair<string, Shop> s in d)
            {
                s.Value.WriteToFile();
            }
        }
        public void Delete()
        {
            Dictionary<string, Shop> d = ReadFromFile();
            d.Remove(email);
            Update(d);
        }
        public void WriteToFile()
        {
            StreamWriter sr = new StreamWriter("Shop_Data.txt", true);
            sr.WriteLine(email + ";" + name + ";" + pw + ";" + cc + ";" + address + ";" + phone);
            sr.Close();
        }
    }
}
