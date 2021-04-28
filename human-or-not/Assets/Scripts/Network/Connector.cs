using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;

public class Connector
{
    WebSocket _socket;

    public void Connect(Session session, string url, Action callback)
    {
        _socket = WebSocketFactory.CreateInstance(url);

        _socket.OnOpen += () =>
        {
            // 연결됐을 경우
            session.Open(_socket, url);
        };

        _socket.Connect();
    }
}
