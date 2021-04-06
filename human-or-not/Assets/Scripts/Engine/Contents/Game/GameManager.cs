using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    Dictionary<ushort, GameObject> _nonPlayers = new Dictionary<ushort, GameObject>();

    public ushort NonPlayerCount { get; set; }
    public ushort PlayerCount { get; set; }

    public Action<int> OnSpawnEvent;

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Manager.Resource.Instaniate(path, parent);
        switch (type)
        {
            case Define.WorldObject.NonPlayer:
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }
}
