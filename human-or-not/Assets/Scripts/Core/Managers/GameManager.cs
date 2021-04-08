using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager
{
    Dictionary<ushort, GameObject> _bots = new Dictionary<ushort, GameObject>();

    public ushort BotCount { get; set; } = 0;
    public ushort PlayerCount { get; set; }

    public void SpawnBots(int count = 10)
    {
        GameObject go = new GameObject() { name = "SpawningPool" };
        for (int i = 0; i < count; i++)
        {
            // Bot Spawn
            GameObject bot = Manager.Game.Spawn(Define.WorldObject.Bot, "Character/Dongdong/Bot", go.transform);
            LocateBots(bot, 50.0f);
        }
    }

    void LocateBots(GameObject go, float spawnRadius = 15.0f, Vector3 spawnPos = default(Vector3))
    {
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>();

        if (spawnPos == default(Vector3))
            spawnPos = Vector3.zero;

        // for. 유효한 path
        NavMeshPath path = new NavMeshPath();
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * spawnRadius;
            randDir.y = 0;
            Vector3 randPos = spawnPos + randDir;

            // 가능한지
            if (nma.CalculatePath(randPos, path))
            {
                go.transform.position = randPos;
                break;
            }
        }
    }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Manager.Resource.Instaniate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Bot:
                // _bots에 추가
                break;
            case Define.WorldObject.Player:
                // _players에 추가
                break;
        }

        return go;
    }

    public void Despawn(ushort id, GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Bot:
                break;
            case Define.WorldObject.Player:
                break;
        }
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Init()
    {
        Manager.UI.Init();
        Manager.Input.Init();
    }
}
