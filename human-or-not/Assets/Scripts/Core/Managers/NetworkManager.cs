using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager
{
    ServerSession _session = new ServerSession();

#if UNITY_EDITOR
    const string _url = "ws://localhost:9536/";
#else
    const string _url = "ws://localhost:9536/";
#endif

    public void Open()
    {
        new Connector().Connect(_url, _session);
    }

    public void Close()
    {
        // 연결돼있다면 연결 종료 보내기
        if (_session.IsConnected)
            _session.Close();
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
