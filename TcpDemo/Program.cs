using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int clientIndex = 0;
            int port = 888;//端口
            IPAddress[] serverIP = Dns.GetHostAddresses("127.0.0.1");//定义IP地址
            IPAddress localAddres = serverIP[0];//Ip地址；
            TcpListener tcpListener = new TcpListener(localAddres, port);//监听套接字
            tcpListener.Start();//开始监听
            Console.WriteLine("服务器启动成功，等待用户接入。。。");//输出信息
            try
            {
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();//每接收一个客服端生成一个TcpClient
                    Task.Run(()=> {
                        var currId = ++clientIndex;
                        NetworkStream networkStream = tcpClient.GetStream();//获取网络数据流
                        BinaryReader reader = new BinaryReader(networkStream);//定义数据读取对象
                        BinaryWriter writer = new BinaryWriter(networkStream);//定义数据写入对象

                        try
                        {
                            while (true)
                            {
                                string strReader = reader.ReadString();//接收消息
                                Console.WriteLine("来自客服端的消息：" + strReader);//输出接收的消息
                                writer.Write(currId.ToString());//向对方发送消息
                            }
                        }
                        catch(Exception ex)
                        {

                        }
                    });
                }
            }
            catch
            {
                Console.WriteLine("接收数据失败");
            }

        }
    }
}

