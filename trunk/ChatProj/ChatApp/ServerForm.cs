using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientServerLib;
using System.Threading;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class ServerForm : Form
    {
        MessageManager manager;
        public ServerForm()
        {
            InitializeComponent();
            try
            {
                manager = new MessageManager("serv", "",viewBox);
                manager.StartObj();
                manager.ServInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                manager.StopObj();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
