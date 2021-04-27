using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager
{
    ServerSession _session = new ServerSession();
    Coroutine _tryConnect = null;

#if UNITY_EDITOR
    const string url = "ws://localhost:9536";
#else
    const string url = "ws://localhost:9536";
#endif

    public void Open(Action callback)
    {
        // 연결중 팝업 띄우기
        LoadingMessage loadingMessage = Manager.UI.ShowPopupUI<LoadingMessage>();

        _tryConnect = new Connector().Connect(_session, url, callback);
    }

    public void Close()
    {
        if (_tryConnect != null)
        {
            Manager.CloseCoroutine(_tryConnect);
            _tryConnect = null;
        }

        // 연결돼있다면 연결 종료 보내기
        if (_session.HasConnected)
            _session.Close("Exit Button Cliked");

    }

    public void Send<T>(Packet packet) where T : Packet
    {
        if (packet == null)
            return;

        T body = packet as T;
        string message = JsonUtility.ToJson(body);

        Send(message);
    }

    public void Send(string message)
    {
        if (_session == null)
            return;

        _session.Send(message);
    }
}
