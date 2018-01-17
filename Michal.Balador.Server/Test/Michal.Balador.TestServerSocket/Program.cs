using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
public class AsynchIOServer
{
    static TcpListener tcpListener = new TcpListener(5150);

    static byte[] Decode(byte[] packet)
    {
        var i = packet.Length - 1;
        while (packet[i] == 0)
        {
            --i;
        }
        var temp = new byte[i + 1];
        Array.Copy(packet, temp, i + 1);

        return temp;
    }


    static void Listeners()
    {
        var flag = "Y";
        var yesNo = Encoding.UTF8.GetBytes(flag);
        Socket socketForClient = tcpListener.AcceptSocket();
        if (socketForClient.Connected)
        {
            Console.WriteLine("Client:" + socketForClient.RemoteEndPoint + " now connected to server.");
            NetworkStream networkStream = new NetworkStream(socketForClient);
            System.IO.StreamWriter streamWriter =    new System.IO.StreamWriter(networkStream);
            System.IO.StreamReader streamReader =    new System.IO.StreamReader(networkStream);

            ////here we send message to client
            //Console.WriteLine("type your message to be recieved by client:");
            //string theString = Console.ReadLine();
            //streamWriter.WriteLine(theString);
            ////Console.WriteLine(theString);
            //streamWriter.Flush();
            socketForClient.Send(yesNo);
            //while (true)
            //{
            //here we recieve client's text if any.
            while (true)
            {
                string theString = streamReader.ReadLine();
                Console.WriteLine("Message recieved by client:" + theString);
                if (theString == "exit" || String.IsNullOrEmpty(theString))
                    break;
            }
            streamReader.Close();
            networkStream.Close();
            streamWriter.Close();
            //}

        }
        socketForClient.Close();
        Console.WriteLine("Press any key to exit from server program");
        Console.ReadKey();
    }

    public static void Main()
    {
        //TcpListener tcpListener = new TcpListener(10);
        tcpListener.Start();
        Console.WriteLine("************This is Server program************");
        Console.WriteLine("Hoe many clients are going to connect to this server?:");
        int numberOfClientsYouNeedToConnect = int.Parse(Console.ReadLine());
        for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
        {
            Thread newThread = new Thread(new ThreadStart(Listeners));
            newThread.Start();
        }



        //Socket socketForClient = tcpListener.AcceptSocket();
        //if (socketForClient.Connected)
        //{
        //    Console.WriteLine("Client now connected to server.");
        //    NetworkStream networkStream = new NetworkStream(socketForClient);
        //    System.IO.StreamWriter streamWriter =
        //    new System.IO.StreamWriter(networkStream);
        //    System.IO.StreamReader streamReader =
        //    new System.IO.StreamReader(networkStream);

        //    ////here we send message to client
        //    //Console.WriteLine("type your message to be recieved by client:");
        //    //string theString = Console.ReadLine();
        //    //streamWriter.WriteLine(theString);
        //    ////Console.WriteLine(theString);
        //    //streamWriter.Flush();

        //    //while (true)
        //    //{
        //        //here we recieve client's text if any.
        //    while (true)
        //    {
        //        string theString = streamReader.ReadLine();
        //        Console.WriteLine("Message recieved by client:" + theString);
        //        if (theString == "exit")
        //            break;
        //    }
        //        streamReader.Close();
        //        networkStream.Close();
        //        streamWriter.Close();
        //    //}

        //}
        //socketForClient.Close();
        //Console.WriteLine("Press any key to exit from server program");
        //Console.ReadKey();
    }
}

//using System;
//using System.Net.Sockets;
//using System.Threading;
//public class Client
//{
//    static public void Main(string[] Args)
//    {
//        TcpClient socketForServer;
//        try
//        {
//            socketForServer = new TcpClient("localHost", 10);
//        }
//        catch
//        {
//            Console.WriteLine(
//            "Failed to connect to server at {0}:999", "localhost");
//            return;
//        }

//        NetworkStream networkStream = socketForServer.GetStream();
//        System.IO.StreamReader streamReader =
//        new System.IO.StreamReader(networkStream);
//        System.IO.StreamWriter streamWriter =
//        new System.IO.StreamWriter(networkStream);
//        Console.WriteLine("*******This is client program who is connected to localhost on port No:10*****");

//        try
//        {
//            string outputString;
//            // read the data from the host and display it
//            {
//                //outputString = streamReader.ReadLine();
//                //Console.WriteLine("Message Recieved by server:" + outputString);

//                //Console.WriteLine("Type your message to be recieved by server:");
//                Console.WriteLine("type:");
//                string str = Console.ReadLine();
//                while (str != "exit")
//                {
//                    streamWriter.WriteLine(str);
//                    streamWriter.Flush();
//                    Console.WriteLine("type:");
//                    str = Console.ReadLine();
//                }
//                if (str == "exit")
//                {
//                    streamWriter.WriteLine(str);
//                    streamWriter.Flush();

//                }

//            }
//        }
//        catch
//        {
//            Console.WriteLine("Exception reading from Server");
//        }
//        // tidy up
//        networkStream.Close();
//        Console.WriteLine("Press any key to exit from client program");
//        Console.ReadKey();
//    }

//    private static string GetData()
//    {
//        //Ack from sql server
//        return "ack";
//    }
//}