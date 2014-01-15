using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientServerLib
{
    public class Profile
    {
        private string login;
        private string password;
        private byte[] recBuffer = new byte[BufferSize];
        public byte[] RecBuffer
        {
            get { return recBuffer;}
            set { recBuffer = value; }
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public const int BufferSize = 256;
        public StringBuilder strbuff = new StringBuilder();

        static int count;

        public Socket socket;
        public Profile(Socket client, string name, string password)
        {
            this.socket = client;
            this.password = password;
            this.login = name;
        }
        public Profile()
        {
            socket = null;
            login = "user" + count.ToString();
            password = count.ToString();
            count++;
        }
        public void PrintProfile()
        {
            Console.WriteLine(login);
            Console.WriteLine(password);
            Console.WriteLine(socket.RemoteEndPoint.ToString());
        }
    }
}
