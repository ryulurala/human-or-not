using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketId
{
    // S: Server's packet, C: Client's packet
    C_CreateRoom = 1,
    C_EnterRoom = 2,
    C_LeaveRoom = 3,
    S_CreateRoom = 4,
    S_EnterRoom = 5,
    S_LeaveRoom = 6,
    S_Spawn = 7,
    S_Despawn = 8,
}

public abstract class Packet
{
    public ushort Protocol;
}

#region Server's packet
[Serializable]
public class S_CreateRoom : Packet
{
    public PlayerInfo user;
    public string roomId;
}

[Serializable]
public class S_EnterRoom : Packet
{
    public PlayerInfo user;
    public string roomId;
}

[Serializable]
public class S_Spawn : Packet
{
    public PlayerInfo[] users;
}
#endregion

#region Client's packet
[Serializable]
public class C_CreateRoom : Packet
{
    public string userName;
    public C_CreateRoom()
    {
        Protocol = (ushort)PacketId.C_CreateRoom;
    }
}

[Serializable]
public class C_EnterRoom : Packet
{
    public string playerName;
    public string roomId;

    public C_EnterRoom()
    {
        Protocol = (ushort)PacketId.C_EnterRoom;
    }
}
#endregion