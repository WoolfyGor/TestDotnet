using System;

using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Numerics;
namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any,8000);
            Console.WriteLine("Server started");
            serverSocket.Start();
            while (true) { 
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                NetworkStream stream =clientSocket.GetStream();
                var player= new PlayerData();
                player.score = 5;
                string data = JsonSerializer.Serialize(player);
                Console.WriteLine(data);
                byte[] bytes =Encoding.ASCII.GetBytes(data); ;
                stream.Write(bytes,0,bytes.Length);
                clientSocket.Close();
            }

            serverSocket.Stop();
            Console.WriteLine("Server stopped");
            Console.ReadLine();
        }

        [Serializable]
        public class PlayerData
        {
            public Vector3 pos { get; set; }
            public float score { get; set; }
            public PlayerData()
            {
                pos = new Vector3(1, 1, 1);
                score = 1.5f;
            }
        }
    }

   
}
