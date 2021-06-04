using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : ObjectInfo
{
    public ushort PlayerId { get { return _objectId; } set { _objectId = value; } }
    public string playerName { get; set; }
}
