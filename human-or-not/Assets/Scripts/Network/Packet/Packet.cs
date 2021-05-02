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
    S_BroadcastLeaveRoom = 4,
    S_PlayerList = 5,
}

public abstract class Packet
{
    public ushort Protocol;
}

#region Server's packet
[Serializable]
public class S_BroadcastEnterRoom : Packet
{
    public ushort playerId;
}

[Serializable]
public class S_BroadcastLeaveRoom : Packet
{
    public ushort playerId;
}

[Serializable]
public class S_PlayerList : Packet
{
    public ushort[] players;
}

#endregion

#region Client's packet
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
    public string roomId;

    public C_EnterRoom(string roomId)
    {
        Protocol = (ushort)PacketId.C_EnterRoom;
        this.roomId = roomId;
    }
}
#endregion