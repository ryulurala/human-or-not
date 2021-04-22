using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager
{
    ServerSession _session = new ServerSession();

    public void Init()
    {
        // IP
        string host = "localhost";
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];

        // Port
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 9536);

        Connector connector = new Connector();
        connector.Connect(endPoint, _session);

        Manager.OpenCoroutine(click());
    }

    IEnumerator click()
    {
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log($"클릭!");
                _session.Send();
            }
        }
    }

    public void OnUpdate()
    {
        // 1frame 마다...

        // List<IPacket> list = PacketQueue.Instance.PopAll();
        // foreach (IPacket pkt in list)
        //     PacketManager.Instance.HandlePacket(_session, pkt);
    }

    public void Send<T>() where T : IPacket
    {
        // _session.Send();
    }
}
