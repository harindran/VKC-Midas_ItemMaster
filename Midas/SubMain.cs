using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VKC
{
    static class SubMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
      //  [STAThread]
        static void Main()
        {
            InitialSettings AddOn = InitialSettings.Instance;
            System.Windows.Forms.Application.Run();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}