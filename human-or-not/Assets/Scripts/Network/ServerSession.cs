using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerSession : Session
{
    public override void OnConnected(Uri url)
    {
        Debug.Log($"Onconnected: {url}");
    }

    public override void OnDisconnected(Uri url)
    {
        Debug.Log($"Disconnected: {url}");
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
