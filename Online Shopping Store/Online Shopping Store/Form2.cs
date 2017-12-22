using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Online_Shopping_Store
{
    partial class Form2 : Form
    {
        Dictionary<string,User> d1;
        Dictionary<string,Shop> d2;
        User u = new User();
        Shop sh = new Shop();
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            d1 = u.ReadFromFile();
            d2 = sh.ReadFromFile();
            int[] day=new int[31];
            for (int i = 0; i < 31; i++)
                day[i] = i + 1;
            comboBox1.DataSource = day;
            int[] mon = new int[12];
            for (int i = 0; i < 12; i++)
                mon[i] = i + 1;
            comboBox2.DataSource = mon;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (usercheck())
            {
                u.WriteToFile();
                this.Close();
            }
            else
                MessageBox.Show("Something Went Wrong");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (shopcheck())
            {
                sh.WriteToFile();
                this.Close();
            }
            else
                MessageBox.Show("Something Went Wrong");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool usercheck()
        {
            bool b = true;
            if (Username.Text != "")
            {
                u.un = Username.Text;
                unamelbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                unamelbl.ForeColor = Color.Red;
            }
            if (Email.Text == REmail.Text && Email.Text.Contains("@") && !d1.ContainsKey(Email.Text) && !d2.ContainsKey(Email.Text))
            {
                u.email = Email.Text;
                emaillbl.ForeColor = Color.SeaGreen;
                remaillbl.ForeColor = Color.SeaGreen;
            }
            else if (Email.Text != REmail.Text)
            {
                b = false;
                remaillbl.ForeColor = Color.Red;
            }
            else
            {
                b = false;
                emaillbl.ForeColor = Color.Red;
                remaillbl.ForeColor = Color.SeaGreen;
            }
            if (Password.Text == RPassword.Text && Password.Text.Length >= 6)
            {
                u.pw = Password.Text;
                pwlbl.ForeColor = Color.SeaGreen;
            }
            else if (Password.Text != RPassword.Text)
            {
                b = false;
                rpwlbl.ForeColor = Color.Red;
            }
            else
            {
                b = false;
                pwlbl.ForeColor = Color.Red;
                rpwlbl.ForeColor = Color.SeaGreen;
            }
            if (CreditCard.Text.Length == 16)
            {
                u.cc = CreditCard.Text;
                ccrdlbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                ccrdlbl.ForeColor = Color.Red;
            }
            u.address = Address.Text;
            u.phone = Phone.Text;
            if (Male.Checked)
            {
                u.gender = "Male";
                genderlbl.ForeColor = Color.SeaGreen;
            }
            else if (Female.Checked)
            {
                u.gender = "Female";
                genderlbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                genderlbl.ForeColor = Color.Red;
            }
            if (textBox1.Text != "")
            {
                u.bd = comboBox1.Text + "/" + comboBox2.Text + "/" + textBox1.Text;
                bdlbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                bdlbl.ForeColor = Color.Red;
            }
            return b;
        }
        private bool shopcheck()
        {
            bool b = true;
            if (Sname.Text != "")
            {
                sh.name = Sname.Text;
                Snamelbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                Snamelbl.ForeColor = Color.Red;
            }
            if (Semail.Text == Sremail.Text && Semail.Text.Contains("@") && !d1.ContainsKey(Semail.Text) && !d2.ContainsKey(Semail.Text))
            {
                sh.email = Semail.Text;
                Semaillbl.ForeColor = Color.SeaGreen;
                Sremaillbl.ForeColor = Color.SeaGreen;
            }
            else if (Semail.Text != Sremail.Text)
            {
                b = false;
                Sremaillbl.ForeColor = Color.Red;
            }
            else
            {
                b = false;
                Semaillbl.ForeColor = Color.Red;
                Sremaillbl.ForeColor = Color.SeaGreen;
            }
            if (Spw.Text == Srpw.Text && Spw.Text.Length >= 6)
            {
                sh.pw = Spw.Text;
                Spwlbl.ForeColor = Color.SeaGreen;
            }
            else if (Spw.Text != Srpw.Text)
            {
                b = false;
                Srpwlbl.ForeColor = Color.Red;
            }
            else
            {
                b = false;
                Spwlbl.ForeColor = Color.Red;
                Srpwlbl.ForeColor = Color.SeaGreen;
            }
            if (Sccrd.Text.Length == 16)
            {
                sh.cc = Sccrd.Text;
                Sccrdlbl.ForeColor = Color.SeaGreen;
            }
            else
            {
                b = false;
                Sccrdlbl.ForeColor = Color.Red;
            }
            sh.address = Saddress.Text;
            sh.phone = Sphone.Text;
            return b;
        }
    }
}
