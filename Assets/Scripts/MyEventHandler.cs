using UnityEngine;
using System.Net.Sockets;
using UnityNetwork;

public class ChatHandler : MyEventHandler
{

    TCPPeer peer = null;
    Socket socket = null;

    // Use this for initialization
    public void ConnectToServer()
    {

        // 连接到服务器
        peer = new TCPPeer(this);
        socket = peer.Connect("47.120.4.88", 8000);
       // socket = peer.Connect("127.0.0.1", 8000);
    }

    // 发送聊天消息
    public void SendMessage(Packet packet)
    {
        TCPPeer.Send(socket, packet);
    }

}// end file