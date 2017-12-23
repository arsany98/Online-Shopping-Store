using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Online_Shopping_Store
{
    public partial class Form3 : Form
    {
        User user;
        Product p=new Product();
        Cart c = new Cart();
        List<Product> l;
        Dictionary<string, List<Cart>> d;
        float t = 0;
        public Form3(User u)
        {
            user = u;
            l=p.ReadFromFile();
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello, " + user.un;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < l.Count; i++)
            {
                object[] items = {l[i].image, l[i].brand, l[i].name,l[i].price};
                dataGridView1.Rows.Add(items);
            }
            homepnl.BringToFront();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Cart c = new Cart();
            if (dataGridView1.SelectedRows.Count != 0)
            {
                c.uemail = user.email;
                string productname = (string)dataGridView1.SelectedRows[0].Cells["Pname"].Value;
                c.quantity = (int)numericUpDown1.Value;
                
                for (int i = 0; i < l.Count; i++)
                    if (l[i].name == productname)
                    {
                        if (c.Add(l[i]))
                        {
                            p.Update(l);
                            MessageBox.Show("Product has been added to your Cart Succesfully.");
                        }
                        else
                            MessageBox.Show("There is Only " + l[i].quantity + " From This Product");
                        break;
                    }
            }
            else
                MessageBox.Show("No Product Selected!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].category == s.Text)
                {
                    object[] items = { l[i].image, l[i].brand, l[i].name, l[i].price };
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
                object[] items = { l[i].image, l[i].brand, l[i].name, l[i].price};
                dataGridView1.Rows.Add(items);
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


        private void button14_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Product> ls = new List<Product>();
            ls = p.Search(textBox1.Text,l);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ls.Count; i++)
            {
                object[] items = { ls[i].image, ls[i].brand, ls[i].name, ls[i].price };
                dataGridView1.Rows.Add(items);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            homepnl.BringToFront();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] day = new int[31];
            for (int i = 0; i < 31; i++)
                day[i] = i + 1;
            daycb.DataSource = day;
            int[] mon = new int[12];
            for (int i = 0; i < 12; i++)
                mon[i] = i + 1;
            monthcb.DataSource = mon;

            Username.Text = user.un;
            CreditCard.Text = user.cc;
            Address.Text = user.address;
            Phone.Text = user.phone;
            if (user.gender == "Male")
                Male.Checked = true;
            else
                Female.Checked = true;
            string[] bdate = user.bd.Split('/');
            daycb.Text = bdate[0];
            monthcb.Text = bdate[1];
            year.Text = bdate[2];
            profilepnl.BringToFront();
        }
        private void updateprofile_Click(object sender, EventArgs e)
        {
            Dictionary<string, User> d=user.ReadFromFile();
            user.un = Username.Text;
            user.cc = CreditCard.Text;
            user.address = Address.Text;
            user.phone = Phone.Text;
            if (Male.Checked == true)
                user.gender = "Male";
            else
                user.gender = "Female";
            user.bd = daycb.Text + "/" + monthcb.Text + "/" + year.Text;
            d[user.email] = user;
            user.Update(d);
            MessageBox.Show("Profile has been Updated Successfully.");
            Form3_Load(sender,e);
        }

        private void deleteprofile_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Delete your Profile?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                user.Delete();
                this.Close();
                Form1 f = new Form1();
                f.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t = 0;
            d = c.ReadFromFile();
            dataGridView2.Rows.Clear();
            if (d.ContainsKey(user.email))
            {
                string em = user.email;
                for (int i = 0; i < d[em].Count; i++)
                {
                    object[] items = { d[em][i].image, d[em][i].name, d[em][i].unitprice, d[em][i].quantity, d[em][i].total };
                    dataGridView2.Rows.Add(items);
                    t += d[em][i].total;
                }
            }
            totallbl.Text = "Total: " + t.ToString("0.00 EGP");
            cartpnl.BringToFront();
        }
        private void deletecart_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DialogResult dr = MessageBox.Show("Are You Sure You Want To Remove The Product?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string productname = dataGridView2.SelectedRows[0].Cells["Cproduct"].Value.ToString();
                    for(int i = 0; i < l.Count; i++)
                        if(l[i].name==productname)
                        {
                            c.Delete(user.email, l[i]);
                            break;
                        }
                    p.Update(l);
                    button1_Click(sender, e);
                    MessageBox.Show("Product has been Removed Succesfully.");
                }
            }
            else
                MessageBox.Show("No Product Selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form3_Load(sender, e);
            //c.DeleteAll(user.email);
            Form5 f = new Form5(d[user.email],t);
            f.ShowDialog();
        }
    }
}
