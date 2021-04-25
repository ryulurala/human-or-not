using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class PacketManager
{
    Dictionary<ushort, Func<Session, byte[], IPacket>> _makeFunc = new Dictionary<ushort, Func<Session, byte[], IPacket>>();
    Dictionary<ushort, Action<Session, IPacket>> _handler = new Dictionary<ushort, Action<Session, IPacket>>();

    bool _isInit = false;

    public void Init()
    {
        if (_isInit)
            return;

        PacketHandler handler = new PacketHandler();

        // _makeFunc 등록
        _makeFunc.Add((ushort)PacketId.S_ConnectedClient, MakePacket<S_ConnectedClient>);

        // _handler 등록
        _handler.Add((ushort)PacketId.S_ConnectedClient, handler.S_ConnectedClientHandler);

        _isInit = true;
    }

    public void OnRecvPacket(Session session, byte[] data)
    {
        // Protocol 파싱
        Regex regex = new Regex(@"(?:(""Protocol""\:\d+))");
        string str = regex.Match(Encoding.UTF8.GetString(data)).Value;
        if (String.IsNullOrEmpty(str))
            return;

        ushort protocol = Convert.ToUInt16(str.Substring(str.LastIndexOf(':') + 1));

        // MakePacket Call-back 실행
        Func<Session, byte[], IPacket> func = null;
        if (_makeFunc.TryGetValue(protocol, out func))
        {
            // 패킷 조립(MakePacket)
            IPacket packet = func.Invoke(session, data);
            Debug.Log($"Make Packet: {packet.Protocol}");
            HandlePacket(session, packet);
        }
    }

    public void HandlePacket(Session session, IPacket packet)
    {
        // Packet Handling
        Action<Session, IPacket> action = null;
        Debug.Log($"Handle Packet: {packet.Protocol}");
        if (_handler.TryGetValue(packet.Protocol, out action))
            action.Invoke(session, packet);
    }

    T MakePacket<T>(Session session, byte[] bytes) where T : IPacket, new()
    {
        // Deserializing Packet data
        T packet = JsonUtility.FromJson<T>(Encoding.UTF8.GetString(bytes));

        return packet;
    }
}
