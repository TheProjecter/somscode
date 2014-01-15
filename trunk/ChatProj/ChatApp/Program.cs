using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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
            DialogResult res = MessageBox.Show("Желаете создать собственный сервер?",
                "Initialization", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.No)
            {
                Application.Run(new ChatForm());
            }
            else
            {
                Application.Run(new ServerForm());
            }
        }
    }
}
