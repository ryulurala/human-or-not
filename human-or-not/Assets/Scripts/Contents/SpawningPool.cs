using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField] int _nonPlayerCount = 0;
    [SerializeField] int _reserveCount = 0;
    [SerializeField] int _keepNonPlayerCount = 0;
    [SerializeField] Vector3 _spawnPos;
    [SerializeField] float _spawnRadius = 15.0f;
    [SerializeField] float _spawntime = 5.0f;

    public void AddNonPlayerCount(int value) { _nonPlayerCount += value; }
    public void SetKeepMonsterCount(int count) { _keepNonPlayerCount = count; }

    void Start()
    {
        Manager.Game.OnSpawnEvent -= AddNonPlayerCount;
        Manager.Game.OnSpawnEvent += AddNonPlayerCount;
    }

    void Update()
    {
        while (_reserveCount + _nonPlayerCount < _keepNonPlayerCount)
            StartCoroutine(ReserveSpawn());
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawntime));
        // 랜덤으로 0 ~ 5초 후

        // Monster Spawn
        GameObject go = Manager.Game.Spawn(Define.WorldObject.NonPlayer, "NonPlayer");
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>();

        // for. 유효한 path
        NavMeshPath path = new NavMeshPath();

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * _spawnRadius;
            randDir.y = 0;
            Vector3 randPos = _spawnPos + randDir;

            // 유효한지
            if (nma.CalculatePath(randPos, path))
            {
                go.transform.position = randPos;
                break;
            }
        }
        _reserveCount--;
    }
}
