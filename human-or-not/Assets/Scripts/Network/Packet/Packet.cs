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

public interface IPacket
{
    ushort Protocol { get; }
    void Read(string data);
    string Write();
}

public class C_CreateRoom : IPacket
{
    public ushort playerId;

    public ushort Protocol => (ushort)PacketId.C_CreateRoom;

    public void Read(string data)
    {
        // TODO: 읽기
    }

    public string Write()
    {
        // TODO: 쓰기
        string str = "";

        return str;
    }
}

public class C_EnterRoom : IPacket
{
    public ushort playerId;

    public ushort Protocol => (ushort)PacketId.C_EnterRoom;

    public void Read(string data)
    {
        // TODO: 읽기
    }

    public string Write()
    {
        // TODO: 쓰기
        string str = "";

        return str;
    }
}