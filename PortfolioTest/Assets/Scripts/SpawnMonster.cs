using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class SpawnManager : MonoBehaviour
{
    [Header("스폰 포인트")]
    public Transform[] spawnPoints;

    [Header("몬스터 부모")]
    public Transform monstersParent;
    [Header("스테이지 매니져")]
    public StageManager stageManager;
    [Header("스테이지 데이터 넣기")]
    public List<StageData> stages;

    [SerializeField] BattleManager battleManager;
    public void Spawn(int stage)
    {
        //인덱스 위치에서 벗어나면 에러가 뜨는것이 아닌 리턴
        if (stage < 0 || stage >= stages.Count)
        {
            Debug.LogError($"존재하지 않는 Stage : {stage}");
            return;
        }

        StageData data = stages[stage];

        if (data.isBossStage)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject boss = Instantiate(data.boss,point.position,Quaternion.identity,monstersParent);

            MonsterStatus bossMonster = boss.GetComponent<MonsterStatus>();
            stageManager.AddMonster(bossMonster);
            bossMonster.OnDead += battleManager.TakeReward;
            bossMonster.OnDead += stageManager.OnMonsterDead;
            
            return;
        }

        int spawnCount =Random.Range(data.minSpawnCount,data.maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monster =data.monsters[Random.Range(0, data.monsters.Count)];

            Transform point =spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject mon = Instantiate(monster,point.position,Quaternion.identity,monstersParent);
            MonsterStatus monstatus = mon.GetComponent<MonsterStatus>();

            stageManager.AddMonster(monstatus);
            monstatus.OnDead += battleManager.TakeReward;
            monstatus.OnDead += stageManager.OnMonsterDead;

        }

        
    }
}