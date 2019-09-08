using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();//创建一个TcpClient对象，自动分配主机IP地址和端口号
            tcpClient.Connect("127.0.0.1", 888);//连接服务器，其IP和端口号为127.0.0.1和888
            if (tcpClient != null)
            {
                Console.WriteLine("连接服务器成功");
                NetworkStream networkStream = tcpClient.GetStream();//获取网络数据流
                BinaryReader reader = new BinaryReader(networkStream);//定义数据读取对象
                BinaryWriter writer = new BinaryWriter(networkStream);//定义数据写入对象
                string localip = "127.0.0.1";//存储本机IP，默认值为127.0.0.1
                IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());//获取所有Ip地址
                foreach (var item in ips)
                {
                    if (!item.IsIPv6SiteLocal)//如果不是ipv6
                        localip = item.ToString();
                }
                writer.Write(localip + " 你好服务器，我是客服端");//向服务器发送消息
                while (true)
                {
                    try
                    {
                        string strReader = reader.ReadString();//接收服务器发送的数据
                        Thread.Sleep(1000);
                        var msg = $"客户端：{strReader} 时间：{DateTime.Now.ToString("dd HH:mm:ss:ffff")}";
                        Console.WriteLine($"发送消息：{msg}");
                        writer.Write(msg);
                    }
                    catch
                    {
                        break;//出错退出循环
                    }
                }
            }
            else
            {
                Console.WriteLine("连接服务器错误");
            }
        }
    }
}

