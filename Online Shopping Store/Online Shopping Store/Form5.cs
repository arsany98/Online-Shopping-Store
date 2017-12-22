using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
namespace Online_Shopping_Store
{
    public partial class Form5 : Form
    {
        List<Cart> l;
        float total;
        public Form5(List<Cart> lc,float t)
        {
            InitializeComponent();
            l = lc;
            total = t;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = printDialog1.ShowDialog();
            if(dr==DialogResult.OK)
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font f = new Font("Century Gothic",16);
            SolidBrush b = new SolidBrush(Color.Black);
            float fh = f.Height;
            g.DrawString("Online Shopping Store\n", new Font("Century Gothic", 24,FontStyle.Bold), b, 25, 10);
            g.DrawString("Receipt", new Font("Century Gothic", 20,FontStyle.Bold), b, 350, 100);

            float x = 20;
            float y = 180;
            g.DrawString("Name\t\t\t\t\t\tQ.\tUnit\tTotal", f, b, x, y);
            y += fh + 10;
            for (int i = 0; i < l.Count; i++)
            {
                string st=null;
                for (int j = l[i].name.Length/6; j < 7; j++)
                    st += "\t";
                g.DrawString(l[i].name+st+l[i].quantity+"\t"+l[i].unitprice+"\t"+l[i].total, f, b, x, y);
                y += fh + 10;
            }
            g.DrawString("Total: "+ total.ToString("C2") , f, b, 600, y+100);
        }
    }
}
