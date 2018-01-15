using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.SimpleMessage
{
    public class SocketClientTest
    {
        private Socket socket;
        private string _username, _password;
        private int port;
        private int recvTimeout;
        private CONNECTION_STATUS loginStatus;

        public bool CreateUser(string jid, string nickname = "")
        {
           
            return true;
        }
        public SocketClientTest(string username, string password)
        {
            _username = username;
                _password = password;
               port = 5150;
            recvTimeout = 1000;
            this.loginStatus = CONNECTION_STATUS.CONNECTED;
            //success
            this.fireOnConnectSuccess();

        }
       
        public void Connect()
        {
            try
            {
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           
                this.socket.Connect("localhost", this.port);
                this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, this.recvTimeout);
            }
            catch (Exception ex)
            {
                throw  ex;
            }

            if (!this.socket.Connected)
                throw new ConnectionException("Cannot connect");
        }

        public void Login(bool isValid)
        {
            string m = "OK";
            byte[] data=Encoding.ASCII.GetBytes(m);
            //test.SendData(new byte[0]);
            //System.Threading.Thread.Sleep(100);
            if(!isValid)
                m = "FAILED";

            this.SendData(data);
            this.pollMessage();

            if (this.loginStatus != CONNECTION_STATUS.LOGGEDIN)
            {
                //oneshot failed
                // this.SendData(this.BinWriter.Write(authResp, false));
                this.fireOnLoginFailed("failed!!!!!");
            }
            else
            {
                this.fireOnLoginSuccess("ok", null);
            }
            
        }

        public string SendMessage(string to, string txt)
        {
          var data=   Encoding.UTF8.GetBytes(to+ " "+txt);
            this.Socket_send(data);
            return "";
        }

        public bool pollMessage()
        {
            if (this.loginStatus == CONNECTION_STATUS.CONNECTED || this.loginStatus == CONNECTION_STATUS.LOGGEDIN)
            {
                int toRead = 4090;
        
               var data= this.Socket_read(toRead);
                var yd=Encoding.UTF8.GetString(data);

                Console.WriteLine("data={0}", yd);
                
                this.loginStatus = yd=="Y"? CONNECTION_STATUS.LOGGEDIN: CONNECTION_STATUS.UNAUTHORIZED;
                return true;
            }
            return false;
        }

        private byte[] Socket_read(int length)
        {
            if (!socket.Connected)
            {
                throw new ConnectionException("Socket not connected");
            }

            var buff = new byte[length];
            int receiveLength = 0;
            do
            {
                try
                {
                    receiveLength = socket.Receive(buff, 0, length, 0);
                }
                catch (SocketException excpt)
                {
                    throw new ConnectionException(String.Format("Socket exception: {0}", excpt.Message), excpt);
                }

                //sleep to prevent CPU intensive loop
                if (receiveLength <= 0)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            while (receiveLength <= 0);

            byte[] tmpRet = new byte[receiveLength];
            if (receiveLength > 0)
                Buffer.BlockCopy(buff, 0, tmpRet, 0, receiveLength);
            return tmpRet;
        }

        public void SendData(byte[] data)
        {
            try
            {
                Socket_send(data);
            }
            catch (ConnectionException)
            {
                this.Disconnect();
            }
        }

        private void Socket_send(byte[] data)
        {
            if (this.socket != null && this.socket.Connected)
            {
                this.socket.Send(data);
            }
            else
            {
                throw new ConnectionException("Socket not connected");
            }
        }

        public void Disconnect(Exception ex = null)
        {
            if (this.socket != null)
            {
                this.socket.Close();
            }
            this.loginStatus = CONNECTION_STATUS.DISCONNECTED;
            this.fireOnDisconnect(ex);
        }


        public enum CONNECTION_STATUS
        {
            UNAUTHORIZED,
            DISCONNECTED,
            CONNECTED,
            LOGGEDIN
        }
        //events
        public event ExceptionDelegate OnDisconnect;
        protected void fireOnDisconnect(Exception ex)
        {
            if (this.OnDisconnect != null)
            {
                this.OnDisconnect(ex);
            }
        }

        public event NullDelegate OnConnectSuccess;
        protected void fireOnConnectSuccess()
        {
            if (this.OnConnectSuccess != null)
            {
                this.OnConnectSuccess();
            }
        }

        public event ExceptionDelegate OnConnectFailed;
        protected void fireOnConnectFailed(Exception ex)
        {
            if (this.OnConnectFailed != null)
            {
                this.OnConnectFailed(ex);
            }
        }

        public event LoginSuccessDelegate OnLoginSuccess;
        protected void fireOnLoginSuccess(string pn, byte[] data)
        {
            if (this.OnLoginSuccess != null)
            {
                this.OnLoginSuccess(pn, data);
            }
        }

        public event StringDelegate OnLoginFailed;
        protected void fireOnLoginFailed(string data)
        {
            if (this.OnLoginFailed != null)
            {
                this.OnLoginFailed(data);
            }
        }



        public event OnErrorDelegate OnError;
        protected void fireOnError(string id, string from, int code, string text)
        {
            if (this.OnError != null)
            {
                this.OnError(id, from, code, text);
            }
        }


        public delegate void NullDelegate();
        public delegate void ExceptionDelegate(Exception ex);
        public delegate void LoginSuccessDelegate(string phoneNumber, byte[] data);
        public delegate void StringDelegate(string data);
        public delegate void OnErrorDelegate(string id, string from, int code, string text);


    }
}
