using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    protected ushort _objectId;
    public ushort ObjectId { get { return _objectId; } set { _objectId = value; } }
}
