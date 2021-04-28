using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public void S_Connected(Session session, Packet packet)
    {
        S_Connected body = packet as S_Connected;

        Debug.Log($"S_ConnectedHandler: {body.Protocol}, {body.playerId}");
    }
}
