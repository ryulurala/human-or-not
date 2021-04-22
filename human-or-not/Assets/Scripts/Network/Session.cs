using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public abstract class Session
{
    WebSocket _ws;
    public bool IsConnected { get; private set; } = false;

    public abstract void OnConnected(Uri url);
    public abstract void OnRecv(string data);
    public abstract void OnSend(int length);
    public abstract void OnDisconnected(Uri url);

    public void Open(WebSocket socket)
    {
        IsConnected = true;
        OnConnected(socket.Url);

        _ws = socket;

        // Init
        _ws.OnError += (sender, eventArgs) => { Debug.Log($"{eventArgs.Message}"); Close(); };
        _ws.OnMessage += (sender, eventArgs) => { OnRecv(eventArgs.Data); };
        _ws.OnClose += (sender, eventArgs) => { Debug.Log($"OnClose()"); Close(); };
    }

    public void Close()
    {
        if (_ws == null)
            return;

        IsConnected = false;
        OnDisconnected(_ws.Url);

        _ws.CloseAsync();
        _ws = null;
    }

    public void Send(IPacket packet = null)
    {
        if (_ws == null)
            return;

        try
        {
            _ws.SendAsync("hi!", (bool isCompleted) =>
            {
                if (isCompleted)
                    OnSend(length: 0);
            });
        }
        catch (Exception e)
        {
            Close();

            Debug.LogError(e);
        }
    }
}
