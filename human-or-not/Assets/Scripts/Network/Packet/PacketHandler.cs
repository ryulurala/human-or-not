using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public void S_BroadcastEnterRoom(Session session, Packet packet)
    {
        S_BroadcastEnterRoom body = packet as S_BroadcastEnterRoom;

        Debug.Log($"S_ConnectedClientHandler: {body.Protocol}");
    }

    public void S_PlayerList(Session session, Packet packet)
    {

    }
}
