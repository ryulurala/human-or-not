using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;

public class Connector
{
    public void Connect(Session session, string url, Action action)
    {
        WebSocket socket = WebSocketFactory.CreateInstance(url);

        socket.OnOpen += () =>
        {
            // 연결됐을 경우
            session.Open(socket, url);
            action.Invoke();
        };

        socket.Connect();
    }
}
