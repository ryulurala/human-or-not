using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PacketId
{
    // Server, Client 둘다.
    S_BroadcastEnterGame,
}

public interface IPacket
{
    ushort Protocol { get; }
    void Read(string data);
    string Write();
}

public class S_BroadcastEnterGame : IPacket
{
    public ushort Protocol => (ushort)PacketId.S_BroadcastEnterGame;

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