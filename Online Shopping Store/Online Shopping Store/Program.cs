﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Online_Shopping_Store
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            f.Show();
            Application.Run();
        }
    }
}
