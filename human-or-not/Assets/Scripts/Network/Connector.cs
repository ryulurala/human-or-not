using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;

public class Connector
{
    WebSocket _socket;

    public void Connect(string url, Session session)
    {
        _socket = WebSocketFactory.CreateInstance(url);

        _socket.OnOpen += () =>
        {
            session.Open(_socket, url);
        };

        _socket.Connect();      // Boxing Async
    }
}
