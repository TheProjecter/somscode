using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ConnectionDialog : Form
    {
        public string ip;
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void oKbutton_Click(object sender, EventArgs e)
        {
            ip = ipBox.Text;
            this.Close();
        }
    }
}
