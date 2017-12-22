using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Online_Shopping_Store
{
    
    public partial class Form1 : Form
    {
        Dictionary<string, User> d1;
        Dictionary<string, Shop> d2;
        User u = new User();
        Shop sh = new Shop();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            d1 = u.ReadFromFile();
            d2 = sh.ReadFromFile();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (d1.ContainsKey(Email.Text) && d1[Email.Text].pw == Password.Text)
            {
                Form3 f = new Form3(d1[Email.Text]);
                f.Show();
                this.Close();
            }
            else if(d2.ContainsKey(Email.Text)&& d2[Email.Text].pw == Password.Text)
            {
                Form4 f = new Form4(d2[Email.Text]);
                f.Show();
                this.Close();
            }
            else
                MessageBox.Show("Incorrect Username Or Password.\nRegister If You Don't Have An Account.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
            d1 = u.ReadFromFile();
            d2 = sh.ReadFromFile();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Application.Exit();
        }
    }
    
}
