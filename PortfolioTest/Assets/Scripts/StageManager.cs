using UnityEngine;
using System.Collections.Generic;
public class StageManager : MonoBehaviour
{
    [Header("스폰 매니저 연결")]
    public SpawnManager spawnManager;

    [Header("현재 스테이지")]
    public int stageIndex = 0;

    [Header("현재 살아있는 몬스터")]
    public List<MonsterStatus> aliveMonsters = new();

    void Start()
    {
        StartStage(stageIndex);
    }

    public void StartStage(int index)
    {
        stageIndex = index;

        aliveMonsters.Clear();

        // 스테이지 시작 시 스폰 요청
        spawnManager.Spawn(stageIndex);
    }


    public List<MonsterStatus> GetAliveMonsters()
    {
        aliveMonsters.RemoveAll(m => m == null);
        return aliveMonsters;
    }
    public void AddMonster(MonsterStatus monster)
    {
        aliveMonsters.Add(monster);
    }

    public MonsterStatus ShortMagnitude(Vector3 position)
    {
        MonsterStatus shortmag = null;
        float shortmagDist = float.MaxValue;

        foreach (MonsterStatus monster in aliveMonsters)
        {
            if(monster == null) continue;   

            float dist = (monster.transform.position - position).sqrMagnitude;
            if (dist < shortmagDist)
            {
                shortmagDist = dist;
                shortmag = monster;
            }
        }
        return shortmag;

    }
    public void OnMonsterDead(MonsterStatus monster)
    {
        if(monster ==null) return;
        if(monster.IsDead == false) return;

        aliveMonsters.Remove(monster);

        if (aliveMonsters.Count <= 0)
        {
            if(stageIndex >= spawnManager.stages.Count-1)
            {
                Debug.Log("게임 클리어");
                return;
            }

            Debug.Log("스테이지 클리어!");
            
            StartStage(stageIndex + 1);
        }
    }
}