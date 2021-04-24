using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSession : Session
{
    public override void OnConnected(Uri url)
    {
        Debug.Log($"{url} Onconnected");
    }

    public override void OnDisconnected(Uri url, string message)
    {
        Debug.Log($"{url} Disconnected: {message}");
    }

    public override void OnRecv(string data)
    {
        Debug.Log($"OnRecv: {data}");
    }

    public override void OnSend(int length)
    {
        Debug.Log($"OnSend: {length}");
    }
}
