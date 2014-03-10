using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientServerLib
{
    public class MessageManager
    {
        #region Variables
        static object locker = new object();
        private static object socketObj;
        private static object interfaceObj;
        #endregion
        public MessageManager(string mode, string ip, object _interfaceObj)
        {
            interfaceObj = _interfaceObj;
            if (mode == "clientLoc")
            {
                socketObj = new AsynchronousClient(ip);
            }
            if (mode == "clientExt")
            {
                socketObj = new AsynchronousClient(ip);
            }
            if (mode == "serv")
            {
                socketObj = new AsynchronousSocketListener();
            }
        }
        public void ServInfo()
        {
            try
            {
                AsynchronousSocketListener Server = (AsynchronousSocketListener)socketObj;
                ShowMessage("Информация о сервере:\r");
                ShowMessage("Локальный ip адрес: " + Server.IpAddress.ToString() + "\r");
                ShowMessage("Ip адрес выданный провайдером: " + Server.GetExternalIP() + "\r");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region ObjectStarters
        public void StartObj(string logpas)
        {
            try
            {
                AsynchronousClient Client = (AsynchronousClient)socketObj;
                Client.Start();
                Client.SendMessage(logpas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void StopObj(string stopMess)
        {
            try
            {
                AsynchronousClient Client = (AsynchronousClient)socketObj;
                Client.SendMessage(stopMess);
                Client.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void StartObj()
        { 
            try
            {
                AsynchronousSocketListener Server = (AsynchronousSocketListener)socketObj;
                Server.Start();
                ShowMessage("Сервер был успешно запущен\r");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void StopObj()
        {
            try
            {
                AsynchronousSocketListener Server = (AsynchronousSocketListener)socketObj;
                Server.Stop();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Work with messages
        public static void SendMessage(string content)
        {
            try 
            {
                AsynchronousClient Client = (AsynchronousClient)socketObj;
                Client.Send(content);
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string ReceiveMessage(string content)
        {
            if (content.IndexOf("$") > -1)
            {
                // All the data has been read from the 
                // client.
                string[] mesArr = content.Split('$');
                int i;
                for (i = mesArr.Length - 2; i >= 0; i--)
                {
                    mesArr[i] = mesArr[i].Trim('\0');
                    if (mesArr[i] != "")
                    {
                        return mesArr[i];
                    }
                }
            }
            return null;
        }
        public static void ShowMessage(string msg)
        {
            lock (locker)
            {
                RichTextBox messBox = (RichTextBox)interfaceObj;
                messBox.Text += msg;
            }
        }
        #endregion
    }
}
