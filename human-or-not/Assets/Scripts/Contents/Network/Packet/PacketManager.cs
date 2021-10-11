using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class PacketManager
{
    Dictionary<ushort, Func<Session, byte[], Packet>> _makeFunc = new Dictionary<ushort, Func<Session, byte[], Packet>>();
    Dictionary<ushort, Action<Session, Packet>> _handler = new Dictionary<ushort, Action<Session, Packet>>();

    PacketQueue _queue = new PacketQueue();

    public PacketQueue Queue { get => _queue; }

    public PacketManager()
    {
        Register();
    }

    public void Register()
    {
        // _makeFunc 등록
        _makeFunc.Add((ushort)PacketId.S_CreateRoom, MakePacket<S_CreateRoom>);
        _makeFunc.Add((ushort)PacketId.S_EnterRoom, MakePacket<S_EnterRoom>);
        _makeFunc.Add((ushort)PacketId.S_UserList, MakePacket<S_UserList>);
        _makeFunc.Add((ushort)PacketId.S_LeaveRoom, MakePacket<S_LeaveRoom>);

        // _handler 등록
        _handler.Add((ushort)PacketId.S_CreateRoom, PacketHandler.S_CreateRoom);
        _handler.Add((ushort)PacketId.S_EnterRoom, PacketHandler.S_EnterRoom);
        _handler.Add((ushort)PacketId.S_UserList, PacketHandler.S_UserList);
        _handler.Add((ushort)PacketId.S_LeaveRoom, PacketHandler.S_LeaveRoom);
    }

    public void OnRecvPacket(Session session, byte[] data)
    {
        ushort protocol = ParseProtocol(data);
        if (protocol == 0)
            return;

        // MakePacket Call-back 실행
        Func<Session, byte[], Packet> func = null;
        if (_makeFunc.TryGetValue(protocol, out func))
        {
            // 패킷 조립(MakePacket)
            Packet packet = func.Invoke(session, data);

            // Packet Queue에 push: Unity는 Main thread가 처리해야 함.
            _queue.Push(packet);
        }
    }

    public void HandlePacket(Session session, Packet packet)
    {
        // Packet Handling
        Action<Session, Packet> action = null;
        if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);
    }

    ushort ParseProtocol(byte[] data)
    {
        // Parsing protocol
        string pattern = @"(""Protocol"":\d+)";
        string protocolData = Regex.Match(Encoding.UTF8.GetString(data), pattern).Value;
        if (String.IsNullOrEmpty(protocolData))
            return 0;

        ushort protocol = Convert.ToUInt16(protocolData.Substring(protocolData.LastIndexOf(':') + 1));
        return protocol;
    }

    T MakePacket<T>(Session session, byte[] bytes) where T : Packet
    {
        // Deserializing Packet data
        string jsonStr = Encoding.UTF8.GetString(bytes);
        T packet = JsonUtility.FromJson<T>(jsonStr);

        return packet;
    }
}
