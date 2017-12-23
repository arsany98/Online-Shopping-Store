using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Online_Shopping_Store
{
    public partial class Form4 : Form
    {
        Shop shop;
        Product p = new Product();
        List<Product> l;
        public Form4(Shop s)
        {
            InitializeComponent();
            shop = s;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello, " + shop.name;
            l = p.ReadFromFile();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].shop == shop.name)
                {
                    object[] items = { l[i].image, l[i].brand, l[i].name, l[i].price ,l[i].quantity};
                    dataGridView1.Rows.Add(items);
                }
            }
            homepnl.BringToFront();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DialogResult dr = MessageBox.Show("Are You Sure You Want To Remove The Product?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string productname = dataGridView1.SelectedRows[0].Cells["Pname"].Value.ToString();
                    p.Delete(productname);
                    Form4_Load(sender, e);
                    MessageBox.Show("Product has been deleted Succesfully.");
                }
            }
            else
                MessageBox.Show("No Product Selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].category == s.Text&& l[i].shop==shop.name)
                {
                    object[] items = { l[i].image, l[i].brand, l[i].name, l[i].price,l[i].quantity };
                    dataGridView1.Rows.Add(items);
                }
            }
            panel3.Top = s.Top;
            homepnl.BringToFront();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < l.Count; i++)
            {
                if(l[i].shop==shop.name)
                {
                    object[] items = { l[i].image, l[i].brand, l[i].name, l[i].price, l[i].quantity };
                    dataGridView1.Rows.Add(items);
                }
            }
            panel3.Top = button11.Top;
            homepnl.BringToFront();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            profilepnl.BringToFront();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            homepnl.BringToFront();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Product> ls = new List<Product>();
            ls = p.Search(textBox1.Text, l);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ls.Count; i++)
            {
                object[] items = { ls[i].image, ls[i].brand, ls[i].name, ls[i].price };
                dataGridView1.Rows.Add(items);
            }
        }

        private void updateprofile_Click(object sender, EventArgs e)
        {
            Dictionary<string, Shop> d = shop.ReadFromFile();
            shop.name = Sname.Text;
            shop.cc = Sccrd.Text;
            shop.address = Saddress.Text;
            shop.phone = Sphone.Text;
            d[shop.email] = shop;
            shop.Update(d);
            MessageBox.Show("Profile has been Updated Successfully.");
            Form4_Load(sender, e);
        }

        private void deleteprofile_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Delete your Profile?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                shop.Delete();
                this.Close();
                Form1 f = new Form1();
                f.Show();
            }
        }

        private void browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            imagein.Text = openFileDialog1.FileName;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                string productname = dataGridView1.SelectedRows[0].Cells["Pname"].Value.ToString();
                for (int i = 0; i < l.Count; i++)
                    if (l[i].name == productname)
                    {
                        l[i].quantity += int.Parse(numericUpDown1.Text);
                        if(priceupdate.Text!="")
                            l[i].price = float.Parse(priceupdate.Text);
                        break;
                    }
                p.Update(l);
                button11_Click(sender, e);
                MessageBox.Show("Product has been updated Succesfully.");
            }
            else
                MessageBox.Show("No Product Selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void add_Click(object sender, EventArgs e)
        {
            if (productcheck())
            {
                p.WriteToFile();
                l = p.ReadFromFile();
                MessageBox.Show("Product has been added Successfully");
            }
        }
        bool productcheck()
        {
            bool b = true;
            for (int i = 0; i < l.Count; i++)
                if (l[i].name == namein.Text)
                    b = false;
            if (namein.Text != "" && b)
            {
                p.name = namein.Text;
                namelbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                namelbl.ForeColor = Color.Red;
            }
            if (brandin.Text != "")
            {
                p.brand = brandin.Text;
                brandlbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                brandlbl.ForeColor = Color.Red;
            }
            if (pricein.Text != "" && float.TryParse(pricein.Text, out float pr))
            {
                p.price = float.Parse(pricein.Text);
                pricelbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                pricelbl.ForeColor = Color.Red;
            }
            if (quantityin.Text != "")
            {
                p.quantity = int.Parse(quantityin.Text);
                quantitylbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                quantitylbl.ForeColor = Color.Red;
            }
            if (categoryin.Text != "")
            {
                p.category = categoryin.Text;
                categorylbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                categorylbl.ForeColor = Color.Red;
            }
            p.imagePath = imagein.Text;
            p.shop = shop.name;
            return b;
        }
    }
}
