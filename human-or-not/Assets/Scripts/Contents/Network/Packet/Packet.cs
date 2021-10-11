using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketId
{
    // S: Server's packet, C: Client's packet
    C_CreateRoom = 1,
    C_EnterRoom = 2,
    S_CreateRoom = 11,
    S_EnterRoom = 12,
    S_LeaveRoom = 13,
    S_UserList = 14,
}

public abstract class Packet
{
    public ushort Protocol;
}

#region Interface
[Serializable]
public class UserInfo
{
    public string id;
    public string name;
}
#endregion

#region Server's packet
[Serializable]
public class S_CreateRoom : Packet
{
    public UserInfo user;
    public string roomId;
}

[Serializable]
public class S_EnterRoom : Packet
{
    public UserInfo user;
    public string roomId;
}

[Serializable]
public class S_LeaveRoom : Packet
{
    public UserInfo user;
}

[Serializable]
public class S_UserList : Packet
{
    public UserInfo[] users;
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
    public string userName;
    public string roomId;

    public C_EnterRoom()
    {
        Protocol = (ushort)PacketId.C_EnterRoom;
    }
}
#endregion