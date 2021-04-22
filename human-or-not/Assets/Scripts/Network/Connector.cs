using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using WebSocketSharp;

public class Connector
{
    WebSocket _socket;
    Session _session;
    EndPoint _endPoint;

    public void Connect(IPEndPoint endPoint, Session session)
    {
        _endPoint = endPoint;
        _session = session;
        _socket = new WebSocket($"ws://{endPoint.ToString()}");

        Manager.OpenCoroutine(TryConnect());
    }

    IEnumerator TryConnect()
    {
        int seconds = 0;
        while (seconds < 10)
        {
            _socket.Connect();
            yield return new WaitForSeconds(1.0f);
            seconds++;

            if (_socket.ReadyState == WebSocketState.Open)
            {
                _session.Init(_socket);
                _session.OnConnected(_endPoint);
                break;
            }

            Debug.Log($"Connecting... {_socket.ReadyState} {seconds}");
        }
    }
}
