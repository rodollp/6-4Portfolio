using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("스폰 매니저 연결")]
    public SpawnManager spawnManager;

    [Header("현재 스테이지")]
    public int stageIndex = 0;

    [Header("남은 몬스터 수")]
    public int monsterCount = 0;

    void Start()
    {
        StartStage(stageIndex);
    }

    public void StartStage(int index)
    {
        stageIndex = index;

        // 스테이지 시작 시 스폰 요청
        spawnManager.Spawn(stageIndex);
    }

    public void OnMonsterDead()
    {
        monsterCount--;

        if (monsterCount <= 0)
        {
            Debug.Log("스테이지 클리어!");
            StartStage(stageIndex + 1);
        }
    }
}