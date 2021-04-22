using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    public WebSocket _ws;

    void Start()
    {
        try
        {
            _ws = new WebSocket("ws://localhost:9536");
            _ws.Connect();
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

    void Update()
    {

    }
}
