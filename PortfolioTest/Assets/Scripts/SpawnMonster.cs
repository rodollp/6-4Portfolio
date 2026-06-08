using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("StageManager 연결")]
    public StageManager stageManager;

    [Header("몬스터 프리팹")]
    public GameObject[] monsters;

    [Header("스폰 포인트")]
    public Transform[] spawnPoints;

    [Header("몬스터 저장소")]
    public Transform monstersParent;

    public void Spawn(int stage)
    {
        int spawnCount = 0;

        if (stage == 0) spawnCount = 5;
        if (stage == 1) spawnCount = 8;
        if (stage == 2) spawnCount = 12;

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monster =
                monsters[Random.Range(0, monsters.Length)];

            Transform point =
                spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(monster, point.position, Quaternion.identity, monstersParent);
        }

        stageManager.monsterCount = spawnCount;
    }
}