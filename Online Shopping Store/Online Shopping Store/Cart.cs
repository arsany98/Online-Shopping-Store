using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
namespace Online_Shopping_Store
{
    public class Cart
    {
        public int quantity;
        public float unitprice,total;
        public string name,uemail,imagePath;
        public Image image;
        public Cart()
        {

        }
        public Cart(string[] s)
        {
            uemail = s[0];
            name = s[1];
            quantity = int.Parse(s[2]);
            unitprice = float.Parse(s[3]);
            imagePath = s[4];
            if(File.Exists(imagePath))
                image = Image.FromFile(s[4]);
            total = quantity * unitprice;
        }
        public Dictionary<string,List<Cart>>ReadFromFile()
        {
            Dictionary<string,List<Cart>> d=new Dictionary<string,List<Cart>>();
            if (File.Exists("Cart_Data.txt"))
            {
                StreamReader sr = new StreamReader("Cart_Data.txt");
                while(!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(';');
                    Cart c = new Cart(s);
                    if (d.ContainsKey(s[0]))
                        d[s[0]].Add(c);
                    else
                    {
                        List<Cart> l = new List<Cart>();
                        l.Add(c);
                        d.Add(s[0], l);
                    }
                }
                sr.Close();
            }
            return d;
        }
        public bool Add(Product p)
        {
            Dictionary<string, List<Cart>> d = ReadFromFile();
            if (quantity > p.quantity)
                return false;
            for (int i = 0; i < d[uemail].Count; i++)
            {
                if (d[uemail][i].name == p.name)
                {
                    d[uemail][i].quantity += quantity;
                    Update(d);
                    p.quantity -= quantity;
                    return true;
                }
            }
            name = p.name;
            unitprice = p.price;
            imagePath = p.imagePath;
            WriteToFile();
            p.quantity -= quantity;
            return true;
        }
        public void Update(Dictionary<string,List<Cart>> d)
        {
            StreamWriter sr = new StreamWriter("Cart_Data.txt");
            sr.Flush();
            sr.Close();
            foreach (KeyValuePair<string, List<Cart>> c in d)
            {
                for (int i = 0; i < c.Value.Count; i++)
                {
                    c.Value[i].WriteToFile();
                }
            }
        }
        public void Delete(string email,Product p)
        {
            Dictionary<string, List<Cart>> d = ReadFromFile();
            for (int i = 0; i < d[email].Count; i++)
            {
                if (d[email][i].name == p.name)
                {
                    p.quantity += d[email][i].quantity;
                    d[email].Remove(d[email][i]);
                    break;
                }
            }
            Update(d);
        }
        public void DeleteAll(string email)
        {
            Dictionary<string, List<Cart>> d = ReadFromFile();
            d.Remove(email);
            Update(d);
        }
        public void WriteToFile()
        {
            StreamWriter sr = new StreamWriter("Cart_Data.txt", true);
            sr.WriteLine(uemail + ";" + name + ";" + quantity + ";" + unitprice + ";" + imagePath);
            sr.Close();
        }
        
    }
}
