using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInfo : ObjectInfo
{
    public ushort BotId { get { return _objectId; } set { _objectId = value; } }
}
