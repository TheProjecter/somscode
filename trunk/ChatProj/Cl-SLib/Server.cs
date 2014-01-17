using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClientServerLib
{
    public class AsynchronousSocketListener
    {
        #region Variables
        private static ManualResetEvent allDone =
        new ManualResetEvent(false);
        private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
        new ManualResetEvent(false);

        private Thread threadSend;
        private Thread threadListening;

        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint localEndPoint;

        private int port = 49000;
        private Socket listener;

        public List<Profile> clientList = new List<Profile>();
        public List<string> messageList = new List<string>();
        object locker = new object();
        #endregion
        public AsynchronousSocketListener()
        {
            // Establish the local endpoint for the socket.
            
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        }
        public IPAddress IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        #region Starters
        public void Start()
        {
            try
            {
                threadListening = new Thread(StartListening);
                threadListening.Start();
                threadSend = new Thread(StartSender);
                threadSend.Start();
                
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StartSender()
        {
            connectDone.WaitOne();
            while (true)
            {
                try
                {
                    if (messageList.Count != 0)
                    {
                        lock (locker)
                        {
                            foreach (string msg in messageList)
                            {
                                foreach (Profile user in clientList)
                                {
                                    sendDone.Reset();
                                    Send(user, msg);
                                    sendDone.WaitOne();
                                }
                                Thread.Sleep(10);
                            }
                            messageList.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void StartListening()
        {
            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);


                // Start an asynchronous socket to listen for connections.
                while (true)
                {
                    allDone.Reset();

                    for (int i = 0; i < 10; i++)
                    {
                        listener.BeginAccept(null, 1024,
                            new AsyncCallback(AcceptCallback), listener);
                    }

                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Stop()
        {
            try
            {
                threadSend.Abort();
                threadSend.Join();
                threadListening.Abort();
                threadListening.Join();
                listener.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Functions
        private void CloseConnect(Profile user)
        {
            try
            {
                user.socket.Close();

                lock (locker)
                    clientList.Remove(user);

                SendSystemMessage("Вышел из чата ", user);
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string GetExternalIP()
        {
            string serviceURL = "http://internet.yandex.ru/";

            string serviceTag = "b-info__item b-info__item_type_ip";

            try
            {
                WebClient wc = new WebClient();

                string requestResult = Encoding.UTF8.GetString(wc.DownloadData(serviceURL));

                if (!string.IsNullOrEmpty(requestResult))
                {
                    string[] split1 = requestResult.Split(new string[] { serviceTag }, new StringSplitOptions());

                    string[] split2 = split1[1].Split('<', '>');

                    split1 = split2[1].Split(':');
                    split1[1] = split1[1].Trim('\n', ' ');

                    return split1[1];
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
        #endregion
        #region Callbacks
        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                allDone.Set();

                Profile user = new Profile();

                // Get the socket that handles the client request.
                Socket sock = (Socket)ar.AsyncState;
                int countBytes = -1;
                byte[] buff;
                user.socket = sock.EndAccept(out buff, out countBytes, ar);
                string text = Encoding.Unicode.GetString(buff);
                string[] logAndpPas = text.Split('&');
                user.Login = logAndpPas[0];
                logAndpPas[1] = logAndpPas[1].Remove(logAndpPas[1].Length - 8);
                user.Password = logAndpPas[1];

                lock (locker)
                    clientList.Add(user);

                user.socket.BeginReceive(user.RecBuffer, 0, user.RecBuffer.Length, 0,
                    new AsyncCallback(ReceiveCallback), user);

                MessageManager.ShowMessage(user.Login + "Вошел в чат");
                SendSystemMessage("Вошел в чат", user);

                connectDone.Set();
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendSystemMessage(string msg, Profile from)
        {
            try
            {
                lock (locker)
                    messageList.Add(string.Format("{0} " + msg + "\r$", from.Login));
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ReceiveCallback(IAsyncResult ar)
        {
            Profile user = (Profile)ar.AsyncState;
            String content = String.Empty;
            
            try
            {
                int bytesCount = user.socket.EndReceive(ar);

                if (bytesCount > 0)
                {
                    user.strbuff.Append(Encoding.Unicode.GetString(user.RecBuffer, 0, user.RecBuffer.Length));

                    content = MessageManager.ReceiveMessage(user.strbuff.ToString());
                    MessageManager.ShowMessage(user.Login + ": " + content);

                    if (content == "end session\r")
                    {
                        CloseConnect(user);
                    }

                    lock (locker)
                        messageList.Add(user.Login + ":" + content + "$");

                    user.RecBuffer = new byte[256];
                    user.socket.BeginReceive(user.RecBuffer, 0, user.RecBuffer.Length, 0,
                        new AsyncCallback(ReceiveCallback), user);
                }
                else CloseConnect(user);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Send(Profile user, string data)
        {
            try
            {
                // Convert the string data to byte data using Unicode encoding.
                byte[] byteData = Encoding.Unicode.GetBytes(data);

                // Begin sending the data to the remote device.
                user.socket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), user);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Profile user = (Profile)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSend = user.socket.EndSend(ar);

                sendDone.Set();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
