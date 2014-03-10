using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientServerLib
{
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] recBuffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
    public class AsynchronousClient
    {
        #region Variables
        // The port number for the remote device.
        private const int port = 49000;

        private Thread threadSend;
        private Thread threadListening;

        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint remoteEP;

        private Socket server;

        // The response from the remote device.
        private String response = String.Empty;
        #endregion
        public AsynchronousClient(string ipAddr)
        {
            //Establish the remote endpoint for the socket.
            string ip = GetExternalIP();
            if (ipAddr != "")
            {
                ipAddress = IPAddress.Parse(ipAddr);
            }
            else
            {
                ipHostInfo = Dns.Resolve(Dns.GetHostName());
                ipAddress = ipHostInfo.AddressList[0];
            }
            remoteEP = new IPEndPoint(ipAddress, port);

            server = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        }
        #region Starters
        public void Start()
        {
            try
            {
                threadSend = new Thread(StartSender);
                threadSend.Start();
                threadListening = new Thread(StartReceive);
                threadListening.Start();
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StartSender()
        {
            // Connect to a remote device.
            try
            {
                //Console.WriteLine("Try to connect...");
                //MessageBox.Show("Try to connect...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                server.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), server);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StartReceive()
        {
            try
            {
                connectDone.WaitOne();
                //Console.WriteLine("Start to Listening");
                //MessageBox.Show("Start to Listening", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (true)
                {
                    receiveDone.Reset();

                    // Create the state object.
                    StateObject state = new StateObject();
                    state.workSocket = server;

                    // Begin receiving the data from the remote device.
                    server.BeginReceive(state.recBuffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);

                    receiveDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                server.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Functions
        public void SendMessage(string message)
        {
            try
            {
                connectDone.WaitOne(1000);
                Send(message);
                sendDone.WaitOne(1000);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Send(String data)
        {
            try
            {
                // Convert the string data to byte data using Unicode encoding.
                byte[] byteData = Encoding.Unicode.GetBytes(data);

                // Begin sending the data to the remote device.
                server.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), server);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CloseConnect()
        {
            try
            {
                server.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetExternalIP()
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
        #region Callbakcs
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                //Console.WriteLine("Connected to " + client.RemoteEndPoint.ToString());
                //MessageBox.Show("Connected to " + client.RemoteEndPoint.ToString(), "Message",
                //    MessageBoxButtons.OK, MessageBoxIcon.Information);
                connectDone.Set();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;

                // Read data from the remote device.
                int bytesRead = state.workSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.Unicode.GetString(
                        state.recBuffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read 
                    // more data.
                    content = MessageManager.ReceiveMessage(state.sb.ToString());
                    MessageManager.ShowMessage(content);
                    receiveDone.Set();
                }
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
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
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