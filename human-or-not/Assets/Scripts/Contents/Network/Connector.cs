using System;
using System.Collections;
using System.Collections.Generic;
using NativeWebSocket;
using UnityEngine;

public class Connector
{
    public async void Connect(Session session, string url, Action action)
    {
        WebSocket socket = new WebSocket(url);

        socket.OnOpen += () =>
        {
            // 연결됐을 경우
            session.Open(socket, url);
            action.Invoke();
        };

        await socket.Connect();
    }
}
