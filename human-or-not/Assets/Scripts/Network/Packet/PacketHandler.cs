using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public void S_ConnectedClientHandler(Session session, IPacket packet)
    {
        S_ConnectedClient body = packet as S_ConnectedClient;

        Debug.Log($"S_ConnectedClientHandler: {body.Protocol}, {body.playerId}");
    }
}
