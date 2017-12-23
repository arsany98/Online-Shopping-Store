using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
namespace Online_Shopping_Store
{
    public class Product
    {
        public string category, name, brand, shop, imagePath;
        public float price;
        public int quantity;
        public Image image;
        public Product()
        {

        }
        public Product(string[] s)
        {
            name = s[0];
            brand = s[1];
            price = float.Parse(s[2]);
            quantity = int.Parse(s[3]);
            category = s[4];
            imagePath = s[5];
            if(File.Exists(imagePath))
            {
                image = Image.FromFile(imagePath);
            }
            shop = s[6];
        }
        public List<Product> ReadFromFile()
        {
            List<Product> l = new List<Product>();
            if (File.Exists("Product_Data.txt"))
            {
                StreamReader sr = new StreamReader("Product_Data.txt");
                while (!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(';');
                    Product p = new Product(s);
                    l.Add(p);
                }
                sr.Close();
            }
            return l;
        }

        public void Update(List<Product> l)
        {
            StreamWriter sr = new StreamWriter("Product_Data.txt");
            sr.Flush();
            sr.Close();
            for (int i = 0; i < l.Count; i++)
                l[i].WriteToFile();
        }
        public void Delete(string productname)
        {
            List<Product> l = ReadFromFile();
            for (int i = 0; i < l.Count; i++)
                if (l[i].name == productname)
                    l.Remove(l[i]);
            Update(l);
            
        }
        public void WriteToFile()
        {
            StreamWriter sr = new StreamWriter("Product_Data.txt", true);
            sr.WriteLine(name + ";" + brand + ";" + price + ";" + quantity + ";" + category + ";" + imagePath + ";" + shop);
            sr.Close();
        }
        public List<Product> Search(string x,List<Product> ob)
        {
            List<Product> l=new List<Product>();
            for (int i = 0; i < ob.Count; i++)
            {
                if (ob[i].name.IndexOf(x,StringComparison.OrdinalIgnoreCase) >=0 || ob[i].category.IndexOf(x,StringComparison.OrdinalIgnoreCase)>=0 || ob[i].brand.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    l.Add(ob[i]);
                }
            }
            return l;
        }
    }
}
