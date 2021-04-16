using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager
{
    public HashSet<BotInfo> Bots = new HashSet<BotInfo>();
    public HashSet<ObjectInfo> Objects = new HashSet<ObjectInfo>();

    public ushort BotCount { get; set; } = 5;
    public ushort PlayerCount { get; set; }

    public void Init()
    {
        Manager.Pool.Init();
        Manager.Input.Init();

        Manager.UI.ShowSceneUI<GameSceneUI>();
    }

    public GameObject SpawnTouchDown(int count = 5)
    {
        GameObject root = new GameObject() { name = "TouchDowns" };
        for (int i = 0; i < count; i++)
        {
            GameObject go = Spawn("Contents/TouchDown", Define.WorldObject.Unknown, root.transform);
            TouchDown touchDown = go.GetOrAddComponent<TouchDown>();

            Vector3 resultPos;
            if (Manager.Game.RandomPoint(Vector3.zero, 200.0f, out resultPos, routineCount: 1000))
                touchDown.transform.position = resultPos;
        }

        return root;
    }

    public GameObject SpawnPlayer(Define.Character type = Define.Character.Dongdong)  // Default: Dongdong
    {
        string characterName = GetCharacterName(type);

        // Spawn player
        GameObject go = Spawn($"Character/{characterName}/Player", Define.WorldObject.Player);

        return go;
    }

    public GameObject SpawnBots(Define.Character type = Define.Character.Dongdong)    // Default: Dongdong
    {
        string characterName = GetCharacterName(type);

        GameObject root = new GameObject() { name = "SpawningPool" };
        for (int i = 0; i < BotCount; i++)
        {
            // Bot Spawn
            GameObject bot = Spawn($"Character/{characterName}/Bot", Define.WorldObject.Bot, root.transform);

            Vector3 resultPos;
            if (Manager.Game.RandomPoint(Vector3.zero, 100.0f, out resultPos, routineCount: 1000))
                bot.transform.position = resultPos;
        }

        return root;
    }

    public GameObject Spawn(string path, Define.WorldObject type = Define.WorldObject.Unknown, Transform parent = null)
    {
        GameObject go = Manager.Resource.Instaniate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Unknown:
                ObjectInfo objectInfo = go.GetOrAddComponent<ObjectInfo>();
                Objects.Add(objectInfo);
                break;
            case Define.WorldObject.Bot:
                BotInfo botInfo = go.GetOrAddComponent<BotInfo>();
                Objects.Add(botInfo);
                break;
            case Define.WorldObject.Player:
                PlayerInfo playerInfo = go.GetOrAddComponent<PlayerInfo>();
                Objects.Add(playerInfo);
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


    public bool RandomPoint(Vector3 center, float range, out Vector3 result, int routineCount = 30)
    {
        for (int i = 0; i < routineCount; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
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
