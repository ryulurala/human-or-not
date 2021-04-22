using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Connector
{
    WebSocket _socket;
    Session _session;

    public void Connect(string url, Session session)
    {
        _session = session;
        _socket = new WebSocket(url);
        _socket.OnOpen += (sender, e) => { _session.Open(_socket); };

        Manager.OpenCoroutine(TryConnect());
    }

    IEnumerator TryConnect()
    {
        _socket.ConnectAsync();

        int seconds = 0;
        while (seconds < 10)
        {
            if (_socket.ReadyState == WebSocketState.Open)
                break;

            Debug.Log($"Connecting... {_socket.ReadyState} {_socket.Url}");
            yield return new WaitForSeconds(1.0f);
            seconds++;

            // 닫혀있을 경우, 한 번 더 시도.
            if (_socket.ReadyState == WebSocketState.Closed)
                _socket.ConnectAsync();
        }
    }
}
