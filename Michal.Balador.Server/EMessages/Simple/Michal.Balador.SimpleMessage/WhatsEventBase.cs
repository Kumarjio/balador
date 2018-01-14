using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace Michal.Balador.SimpleMessage
{
    public class WhatsEventBase 
    {
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
