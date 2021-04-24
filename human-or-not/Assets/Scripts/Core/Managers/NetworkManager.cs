using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager
{
    ServerSession _session = new ServerSession();
    Action _callback = null;

#if UNITY_EDITOR
    const string url = "ws://localhost:9536";
#else
    const string url = "ws://localhost:9536";
#endif

    public void Open(Action callback)
    {
        _callback = callback;

        // 연결중 팝업 띄우기
        Manager.UI.ShowPopupUI<LoadingMessage>();

        Manager.OpenCoroutine(TryConnect());
    }

    public void Close()
    {
        // 연결돼있다면 연결 종료 보내기
        if (_session.HasConnected)
            _session.Close("Exit Button Cliked");

        _callback = null;
    }

    public void OnUpdate()
    {
        // 1frame 마다...

        // List<IPacket> list = PacketQueue.Instance.PopAll();
        // foreach (IPacket pkt in list)
        //     PacketManager.Instance.HandlePacket(_session, pkt);
    }

    public void Send<T>(IPacket packet) where T : IPacket
    {
        if (packet == null)
            return;

        T body = (T)packet;
        string message = JsonUtility.ToJson(body);

        Send(message);
    }

    public void Send(string message)
    {
        if (_session == null)
            return;

        _session.Send(message);
    }

    IEnumerator TryConnect()
    {
        new Connector().Connect(url, _session);
        while (!_session.HasConnected)
        {
            yield return null;
        }
        _callback.Invoke();
    }
}
