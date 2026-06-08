using System;
using UnityEngine;
[System.Serializable]
public struct Reward
{
    public int _gold;
    public int _expReward;

    public Reward(int gold, int expReward)
    {
        _gold = gold;
        _expReward = expReward;
    }
}



public class MonsterStatus : Creture
{

    [Header("몬스터 처치 시 드랍")]
    [SerializeField] Reward reward;

    public Reward Reward => reward;

    public event Action<MonsterStatus> OnDead;

    int currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{Name}이(가) {damage}만큼 피해를 입었습니다");
        if (currentHp <= 0)
        {
            Die();
        }

    }

    protected override void Die()
    {
        StageManager stage = FindAnyObjectByType<StageManager>();
        stage.OnMonsterDead();
        currentHp = 0;
        Debug.Log($"{Name} 사망");

        
        OnDead?.Invoke( this );
        Destroy(gameObject);

    }

}
