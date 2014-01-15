using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AsynchronousClient Client = new AsynchronousClient();
            Thread threadSend = new Thread(Client.StartClient);
            Thread threadListening = new Thread(Client.Receive);
            string cmd = null;
            string profInfo;
            Console.WriteLine("Login:");
            profInfo = Console.ReadLine();
            Console.WriteLine("Password:");
            profInfo += "&" + Console.ReadLine();
            Console.WriteLine();
            while (true)
            {
                cmd = Console.ReadLine();
                if (cmd == "start")
                {
                    threadSend.Start();
                    threadListening.Start();
                    threadListening.IsBackground = true;
                    Client.SendMessage(profInfo);
                }
                else
                {
                    if (cmd != "exit")
                    {
                        Client.SendMessage(cmd+"$");
                    }
                    else
                    {
                        Client.SendMessage(cmd);
                        Client.CloseConnect();
                        threadSend.Interrupt();
                        threadListening.Interrupt();
                        threadSend.Abort();
                        threadListening.Abort();
                        break;
                    }
                }
                cmd = null;
            }
        }
    }
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] recBuffer = new byte[BufferSize];
        public byte[] sendBuffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
    public class AsynchronousClient
    {
        #region Variables
        // The port number for the remote device.
        private const int port = 49000;

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

        #region Functions
        public void StartClient()
        {
            // Connect to a remote device.
            try
            {
               Console.WriteLine("Try to connect...");
                //Establish the remote endpoint for the socket.
                ipHostInfo = Dns.Resolve(Dns.GetHostName());
                ipAddress = ipHostInfo.AddressList[0];
                remoteEP = new IPEndPoint(ipAddress, port);

                server = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                server.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), server);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void SendMessage(string message)
        {
            try
            {
                connectDone.WaitOne();
                Send(message);
                sendDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Send(String data)
        {
            // Convert the string data to byte data using Unicode encoding.
            byte[] byteData = Encoding.Unicode.GetBytes(data);

            // Begin sending the data to the remote device.
            server.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), server);
        }
        public void Receive()
        {
            try
            {
                connectDone.WaitOne();
                Console.WriteLine("Start to Listening");
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
                Console.WriteLine(e.ToString());
            }
        }
        public void CloseConnect()
        {
            // Release the socket.
            try
            {
                server.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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

                Console.WriteLine("Connected to " + client.RemoteEndPoint.ToString());
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                    content = state.sb.ToString();
                    if (content.IndexOf("$") > -1)
                    {
                        // All the data has been read from the 
                        // client. Display it on the console.
                        string[] mesArr = content.Split('$');
                        int i;
                        for (i = mesArr.Length - 2; i >= 0; i--)
                        {
                            if (mesArr[i] != "")
                            {
                                Console.WriteLine(mesArr[i]);
                                break;
                            }
                        }
                        state.workSocket.BeginReceive(state.recBuffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
                        // Not all data received. Get more.
                        state.workSocket.BeginReceive(state.recBuffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
    }
}
