using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketQueue
{
    Queue<Packet> _packetQueue = new Queue<Packet>();
    object _lock = new object();    // for sub-thread

    public void Push(Packet packet)
    {
        lock (_lock)
        {
            _packetQueue.Enqueue(packet);
        }
    }

    public Packet Pop()
    {
        lock (_lock)
        {
            if (_packetQueue.Count == 0)
                return null;

            return _packetQueue.Dequeue();
        }
    }

    public List<Packet> PopAll()
    {
        List<Packet> packetList = new List<Packet>();

        lock (_lock)
        {
            while (_packetQueue.Count > 0)
                packetList.Add(_packetQueue.Dequeue());
        }

        return packetList;
    }
}
