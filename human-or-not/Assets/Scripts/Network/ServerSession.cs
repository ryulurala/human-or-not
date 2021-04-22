using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerSession : Session
{
    public override void OnConnected(EndPoint endPoint)
    {
        Debug.Log($"Onconnected: {endPoint}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Debug.Log($"Disconnected: {endPoint}");
    }

    public override void OnRecv(string data)
    {
        Debug.Log($"OnRecv: {data}");
    }

    public override void OnSend(int numOfBytes)
    {
        Debug.Log($"OnSend: {numOfBytes}");
    }
}
