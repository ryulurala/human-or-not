using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager
{
    public Dictionary<ushort, GameObject> Bots = new Dictionary<ushort, GameObject>();

    public ushort BotCount { get; set; } = 5;
    public ushort PlayerCount { get; set; }

    public void Init()
    {
        Manager.UI.Init();
        Manager.Input.Init();
    }

    public GameObject SpawnPlayer(Define.Character type = Define.Character.Dongdong)  // Default: Dongdong
    {
        string characterName = GetCharacterName(type);

        // Spawn player
        GameObject go = Spawn(Define.WorldObject.Player, $"Character/{characterName}/Player");

        return go;
    }

    public GameObject SpawnBots(Define.Character type = Define.Character.Dongdong)    // Default: Dongdong
    {
        string characterName = GetCharacterName(type);

        GameObject go = new GameObject() { name = "SpawningPool" };
        for (int i = 0; i < BotCount; i++)
        {
            // Bot Spawn
            GameObject bot = Spawn(Define.WorldObject.Bot, $"Character/{characterName}/Bot", go.transform);
            LocateBots(bot, 100.0f);
        }

        return go;
    }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Manager.Resource.Instaniate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Bot:
                // TODO: _bots에 추가
                break;
            case Define.WorldObject.Player:
                // TODO: _players에 추가
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
                // TODO: _bots에 삭제
                break;
            case Define.WorldObject.Player:
                // TODO: _players에 삭제
                break;
        }
    }

    public void LocateBots(GameObject go, float spawnRadius = 15.0f, Vector3 spawnPos = default(Vector3))
    {
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>();

        if (spawnPos == default(Vector3))
            spawnPos = Vector3.zero;

        // for. 유효한 path
        NavMeshPath path = new NavMeshPath();
        for (int i = 0; i < 500; i++)   // true or 100
        {
            Vector3 randPos = spawnPos + Random.insideUnitSphere * spawnRadius;

            // 가능한지
            if (nma.CalculatePath(randPos, path))
            {
                go.transform.position = randPos;
                return;
            }
        }

        // 500번으로도 안되면 (0, 0, 0)
        go.transform.position = Vector3.zero;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public string GetMapName(Define.Map type)
    {
        // Reflaction
        return System.Enum.GetName(typeof(Define.Map), type);
    }

    public string GetCharacterName(Define.Character type)
    {
        // Reflaction
        return System.Enum.GetName(typeof(Define.Character), type);
    }
}
