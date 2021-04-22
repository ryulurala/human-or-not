using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using WebSocketSharp;

public abstract class Session
{
    WebSocket _ws;

    public abstract void OnConnected(EndPoint endPoint);
    public abstract void OnRecv(string data);
    public abstract void OnSend(int numOfBytes);
    public abstract void OnDisconnected(EndPoint endPoint);

    public void Init(WebSocket socket)
    {
        try
        {
            _ws = socket;
            _ws.Send("hi!");

            _ws.OnClose += (sender, eventArgs) =>
            {
                Debug.Log($"Server closed !");
            };
            // _ws.Close();
        }
        catch (Exception e)
        {
            _ws.Close();
            Debug.Log(e);
        }
    }

    void Clear()
    {
        _ws = null;
    }

    public void Send(IPacket packet)
    {

    }
}
