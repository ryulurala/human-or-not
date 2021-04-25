using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketId
{
    // S: Server's packet, C: Client's packet
    S_ConnectedClient = 1,
    C_CreateRoom = 2,
    C_EnterRoom = 3,
    S_BroadcastEnterRoom = 4,
}

public interface IPacket
{
    ushort Protocol { get; }
}

[Serializable]
public class S_ConnectedClient : IPacket
{
    public ushort playerId;

    public ushort Protocol => (ushort)PacketId.S_ConnectedClient;
}

[Serializable]
public class C_CreateRoom : IPacket
{
    public ushort playerId;
    public ushort Protocol => (ushort)PacketId.C_CreateRoom;


}

[Serializable]
public class C_EnterRoom : IPacket
{
    public ushort playerId;

    public ushort Protocol => (ushort)PacketId.C_EnterRoom;
}