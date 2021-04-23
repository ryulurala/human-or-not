using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HybridWebSocket;
using System.Text;

public abstract class Session
{
    WebSocket _ws;

    public Uri Url { get; private set; }
    public bool HasConnected { get; private set; } = false;

    public abstract void OnConnected(Uri url);
    public abstract void OnRecv(string data);
    public abstract void OnSend(int length);
    public abstract void OnDisconnected(Uri url);

    public void Open(WebSocket socket, string url)
    {
        if (socket == null)
            return;

        _ws = socket;
        Url = new Uri(url);

        Init();

        OnConnected(Url);
    }

    public void Close()
    {
        if (HasConnected == false)
            return;

        HasConnected = false;
        _ws.Close();

        Clear();

        OnDisconnected(Url);
    }

    public void Send(IPacket packet = null)
    {
        if (HasConnected == false)
            return;

        try
        {
            _ws.Send(Encoding.UTF8.GetBytes("Hi! WebGL Websocket"));
        }
        catch (Exception e)
        {
            Close();

            Debug.LogError(e);
        }
    }

    void Init()
    {
        if (_ws == null)
            return;

        HasConnected = true;

        _ws.OnError += (string errMsg) =>
        {
            Debug.Log($"{errMsg}");
            Close();
        };

        _ws.OnMessage += (byte[] msg) =>
        {
            OnRecv(Encoding.UTF8.GetString(msg));
        };

        _ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log($"OnClose() {code.ToString()}");
            Close();
        };
    }

    void Clear()
    {
        Url = null;
        _ws = null;
    }
}
