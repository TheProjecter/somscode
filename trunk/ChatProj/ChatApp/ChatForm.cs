using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ClientServerLib;

namespace WindowsFormsApplication1
{
    public partial class ChatForm : Form
    {
        MessageManager manager;
        public ChatForm()
        {
            InitializeComponent();
            if (MessageBox.Show("Желаете ввести ip сервера?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConnectionDialog getIp = new ConnectionDialog();
                getIp.ShowDialog();
                manager = new MessageManager("clientExt", getIp.ip, viewMessBox);
            }
            else
            {
                manager = new MessageManager("clientLoc", "", viewMessBox);
            }
        }
        #region Events
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (LoginBox.Text != "" & passBox.Text != "")
            {
                try
                {
                    manager.StartObj(LoginBox.Text + "&" + passBox.Text);
                    label2.Visible = false;
                    passBox.Visible = false;
                    connectButton.Visible = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                manager.StopObj("end session\r$");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void writeMessBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Thread thr = new Thread(SendTest);
                thr.Start();
                //writeMessBox.Clear();
            }
        }

        private void SendTest()
        {
            int i = 0;
            while (i <= 100)
            {
                //MessageManager.SendMessage(writeMessBox.Text.ToString() + "$");
                MessageManager.SendMessage("testmessage " + i.ToString() + "\r$");
                Thread.Sleep(10);
                i++;
            }
        }
        #endregion
    }
}
