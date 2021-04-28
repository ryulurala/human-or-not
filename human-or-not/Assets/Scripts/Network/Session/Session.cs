using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;
using System.Text;

public abstract class Session
{
    WebSocket _socket;
    Uri _url;

    public abstract void OnConnected(Uri url);
    public abstract void OnRecv(byte[] data);
    public abstract void OnSend(int length);
    public abstract void OnDisconnected(Uri url, string message);

    public void Open(WebSocket socket, string url)
    {
        if (socket == null)
            return;

        _socket = socket;
        _url = new Uri(url);

        OnConnected(_url);
        Init();
    }

    public void Close(string message)
    {
        if (_socket == null)
            return;
        try
        {
            _socket.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        OnDisconnected(_url, message);
        Clear();
    }

    public void Send(string message)
    {
        if (_socket == null)
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
        _socket.OnError += (string errMsg) =>
        {
            Close(errMsg);
        };

        _socket.OnMessage += (byte[] data) =>
        {
            OnRecv(data);
        };

        _socket.OnClose += (WebSocketCloseCode code) =>
        {
            Close(code.ToString());
        };
    }

    void Clear()
    {
        _url = null;
        _socket = null;
    }
}
