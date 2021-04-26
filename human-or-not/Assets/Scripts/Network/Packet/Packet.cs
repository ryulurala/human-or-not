using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketId
{
    // S: Server's packet, C: Client's packet
    C_CreateRoom = 1,
    C_EnterRoom = 2,
    S_BroadcastEnterRoom = 3,
}

[Serializable]
public class Packet
{
    public ushort Protocol;
}

[Serializable]
public class C_CreateRoom : Packet
{
    public C_CreateRoom()
    {
        Protocol = (ushort)PacketId.C_CreateRoom;
    }
}

[Serializable]
public class C_EnterRoom : Packet
{
    public C_EnterRoom()
    {
        Protocol = (ushort)PacketId.C_EnterRoom;
    }
}

[Serializable]
public class S_BroadcastEnterRoom : Packet
{
    public S_BroadcastEnterRoom()
    {
        Protocol = (ushort)PacketId.S_BroadcastEnterRoom;
    }
}
