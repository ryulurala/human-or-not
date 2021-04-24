using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;
using System.Text;

public abstract class Session
{
    WebSocket _socket;

    public Uri Url { get; private set; }
    public bool HasConnected { get; private set; } = false;

    public abstract void OnConnected(Uri url);
    public abstract void OnRecv(string data);
    public abstract void OnSend(int length);
    public abstract void OnDisconnected(Uri url, string message);

    public void Open(WebSocket socket, string url)
    {
        if (socket == null)
            return;

        _socket = socket;
        Url = new Uri(url);

        Init();
        Send("Hi Websocket server !");

        OnConnected(Url);
    }

    public void Close(string message)
    {
        if (HasConnected == false)
            return;

        HasConnected = false;
        _socket.Close();

        Clear();

        OnDisconnected(Url, message);
    }

    public void Send(string message)
    {
        if (HasConnected == false)
            return;

        try
        {
            _socket.Send(Encoding.UTF8.GetBytes(message));

            OnSend(message.Length);
        }
        catch (Exception e)
        {
            Close(e.ToString());
        }
    }

    void Init()
    {
        if (_socket == null)
            return;

        HasConnected = true;

        _socket.OnError += (string errMsg) =>
        {
            Close(errMsg);
        };

        _socket.OnMessage += (byte[] msg) =>
        {
            OnRecv(Encoding.UTF8.GetString(msg));
        };

        _socket.OnClose += (WebSocketCloseCode code) =>
        {
            Close(code.ToString());
        };
    }

    void Clear()
    {
        Url = null;
        _socket = null;
    }
}
