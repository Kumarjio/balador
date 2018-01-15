using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.TestServerSocket
{
    class Program
    {
       
           /// <summary>
            /// Winsock ioctl code which will disable ICMP errors from being propagated to a UDP socket.
            /// This can occur if a UDP packet is sent to a valid destination but there is no socket
            /// registered to listen on the given port.
            /// </summary>
        public const int SIO_UDP_CONNRESET = -1744830452;
        /// <summary>
        /// This routine repeatedly copies a string message into a byte array until filled.
        /// </summary>
        /// <param name="dataBuffer">Byte buffer to fill with string message</param>
        /// <param name="message">String message to copy</param>
        static public void FormatBuffer(byte[] dataBuffer, string message)
        {

            byte[] byteMessage = System.Text.Encoding.ASCII.GetBytes(message);

            int index = 0;
            // First convert the string to bytes and then copy into send buffer
            while (index < dataBuffer.Length)
            {
                for (int j = 0; j < byteMessage.Length; j++)
                {
                    dataBuffer[index] = byteMessage[j];
                    index++;
                    // Make sure we don't go past the send buffer length
                    if (index >= dataBuffer.Length)
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Prints simple usage information.
        /// </summary>
        static void usage()
        {
            Console.WriteLine("Executable_file_name [-l bind-address] [-m message] [-n count] [-p port]");
            Console.WriteLine("                 [-t tcp|udp] [-x size]");
            Console.WriteLine("  -l bind-address        Local address to bind to");
            Console.WriteLine("  -m message             Text message to format into send buffer");
            Console.WriteLine("  -n count               Number of times to send a message");
            Console.WriteLine("  -p port                Local port to bind to");
            Console.WriteLine("  -t udp | tcp           Indicates which protocol to use");
            Console.WriteLine("  -x size                Size  of send and receive buffer");
            Console.WriteLine(" Else, default values will be used...");
        }
        /// <summary>
        /// This is the main routine that parses the command line and invokes the server with the
        /// given parameters. For TCP, it creates a listening socket and waits to accept a client
        /// connection. Once a client connects, it waits to receive a "request" message. The
        /// request is terminated by the client shutting down the connection. After the request is
        /// received, the server sends a response followed by shutting down its connection and
        /// closing the socket. For UDP, the socket simply listens for incoming packets. The "request"
        /// message is a single datagram received. Once the request is received, a number of datagrams
        /// are sent in return followed by sending a few zero byte datagrams. This way the client
        /// can determine that the response has completed when it receives a zero byte datagram.
        /// </summary>
       /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            string textMessage = "Y";
            int localPort = 5150, sendCount = 10, bufferSize = 4096;
            IPAddress localAddress = IPAddress.Any;
            SocketType sockType = SocketType.Stream;
            ProtocolType sockProtocol = ProtocolType.Tcp;         
            Console.WriteLine();
            usage();
            Console.WriteLine();
            // Parse the command line
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    if ((args[i][0] == '-') || (args[i][0] == '/'))
                    {
                        switch (Char.ToLower(args[i][1]))
                        {
                            case 'l':       // Local interface to bind to
                                localAddress = IPAddress.Parse(args[++i]);
                                break;
                            case 'm':       // Text message to put into the send buffer
                                textMessage = args[++i];
                                break;
                            case 'n':       // Number of times to send the response
                                sendCount = System.Convert.ToInt32(args[++i]);
                                break;
                            case 'p':       // Port number for the destination
                                localPort = System.Convert.ToInt32(args[++i]);
                                break;
                            case 't':       // Specified TCP or UDP
                                i++;
                                if (String.Compare(args[i], "tcp", true) == 0)
                                {
                                    sockType = SocketType.Stream;
                                    sockProtocol = ProtocolType.Tcp;
                                }

                               else if (String.Compare(args[i], "udp", true) == 0)
                                {
                                    sockType = SocketType.Dgram;
                                    sockProtocol = ProtocolType.Udp;
                                }
                                else
                                {
                                    usage();
                                    return;
                                }
                                break;
                            case 'x':       // Size of the send and receive buffers
                                bufferSize = System.Convert.ToInt32(args[++i]);
                                break;
                            default:
                                usage();
                                return;
                        }

                    }

                }
                catch
                {
                    usage();
                    return;
                }

            }
            Socket serverSocket = null;
            try
            {

                IPEndPoint localEndPoint = new IPEndPoint(localAddress, localPort), senderAddress = new IPEndPoint(localAddress, 0);
                Console.WriteLine("Server: IPEndPoint is OK...");
                EndPoint castSenderAddress;
                Socket clientSocket;
                byte[] receiveBuffer = new byte[bufferSize], 
                    sendBuffer = new byte[bufferSize];
                int rc;
                FormatBuffer(sendBuffer, textMessage);
                // Create the server socket

                serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);
                Console.WriteLine("Server: Socket() is OK...");

                // Bind the socket to the local interface specified

                serverSocket.Bind(localEndPoint);
                Console.WriteLine("Server: {0} server socket bound to {1}", sockProtocol.ToString(), localEndPoint.ToString());
                if (sockProtocol == ProtocolType.Tcp)
                {
                    // If TCP socket, set the socket to listening
                    serverSocket.Listen(1);
                    Console.WriteLine("Server: Listen() is OK, I'm listening for connection buddy!");
                }      
               else
                {
                   byte[] byteTrue = new byte[4];
                    // Set the SIO_UDP_CONNRESET ioctl to true for this UDP socket. If this UDP socket
                    //    ever sends a UDP packet to a remote destination that exists but there is
                    //    no socket to receive the packet, an ICMP port unreachable message is returned
                    //    to the sender. By default, when this is received the next operation on the
                    //    UDP socket that send the packet will receive a SocketException. The native
                    //    (Winsock) error that is received is WSAECONNRESET (10054). Since we don't want
                    //    to wrap each UDP socket operation in a try/except, we'll disable this error
                    //    for the socket with this ioctl call.
                    byteTrue[byteTrue.Length - 1] = 1;
                    serverSocket.IOControl(SIO_UDP_CONNRESET, byteTrue, null);
                    Console.WriteLine("Server: IOControl() is OK...");
                }
                // Service clients in a loop
                while (true)
                {
                    if (sockProtocol == ProtocolType.Tcp)
                    {
                        // Wait for a client connection
                        clientSocket = serverSocket.Accept();
                        Console.WriteLine("Server: Accept() is OK...");
                        Console.WriteLine("Server: Accepted connection from: {0}", clientSocket.RemoteEndPoint.ToString());
                        // Receive the request from the client in a loop until the client shuts
                       //    the connection down via a Shutdown.
                        Console.WriteLine("Server: Preparing to receive using Receive()...");
                        var flag = "N";

                        while (true)
                        {
                           rc = clientSocket.Receive(receiveBuffer);
                           Console.WriteLine("Server: Read {0} bytes", rc);
                            //if (flag == "Y")
                            //    flag = "N";
                            //else
                            //    flag = "Y";

                            if (rc == 0)
                                break;
                           // System.Threading.Thread.Sleep(100);
                            Console.WriteLine("lior: Preparing to send using Send()...");
                          
                           var receiveBufferRequest = System.Text.Encoding.ASCII.GetString(receiveBuffer);
                            Console.WriteLine("lior:receiveBufferRequest={0}...", receiveBufferRequest);
                            if (receiveBufferRequest.Trim().Contains("OK"))
                                flag = "Y";
                           else if (receiveBufferRequest.Trim().Contains("FAILED"))
                                flag = "N";
                           else
                                flag = "X";
                           
                      
                                Console.WriteLine("lior: Sended {0}...", flag);
                            var yesNo = Encoding.UTF8.GetBytes(flag);
                            rc = clientSocket.Send(yesNo);
                            

                        }
                        // Send the indicated number of response messages

                        Console.WriteLine("Server: Wait...");
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Server: Preparing to send using Send()...");
                        for (int i = 0; i < sendCount; i++)
                        {
                            rc = clientSocket.Send(sendBuffer);
                            Console.WriteLine("Server: Sent {0} bytes", rc);
                        }              
                        // Shutdown the client connection
                        clientSocket.Shutdown(SocketShutdown.Send);
                        Console.WriteLine("Server: Shutdown() is OK...");
                        clientSocket.Close();
                        Console.WriteLine("Server: Close() is OK...");

                    }
                }

            }
           catch (SocketException err)
            {
                Console.WriteLine("Server: Socket error occurred: {0}", err.Message);
            }
            finally
            {
              // Close the socket if necessary
                if (serverSocket != null)
                {
                    Console.WriteLine("Server: Closing using Close()...");
                    serverSocket.Close();
                }
            }
        }
    }
}
