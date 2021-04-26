using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;

public class Connector
{
    WebSocket _socket;

    public Coroutine Connect(Session session, string url, Action callback)
    {
        _socket = WebSocketFactory.CreateInstance(url);

        _socket.OnOpen += () =>
        {
            session.Open(_socket, url);
        };

        return Manager.OpenCoroutine(TryConnect(session, callback));
    }

    IEnumerator TryConnect(Session session, Action callback)
    {
        _socket.Connect();      // Boxing Async
        while (!session.HasConnected)
            yield return null;

        callback.Invoke();
    }
}
