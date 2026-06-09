using System;
using UnityEngine;
[System.Serializable]
public struct Reward
{
    public int _gold;
    public int _expReward;

    public int Gold => _gold;
    public int ExpReward => _expReward;
}



public class MonsterStatus : Creature
{

    [Header("몬스터 처치 시 드랍")]
    [SerializeField] Reward reward;

    public Reward Reward => reward;

    public event Action<MonsterStatus> OnDead;

    public bool IsDead = false;
    public override void TakeDamage(int damage)
    {
       base.TakeDamage(damage);

    }

    protected override void Die()
    {
        if (IsDead) return;
        IsDead = true;
        currentHp = 0;
        Debug.Log($"{Name} 사망");

        
        OnDead?.Invoke( this );
        Destroy(gameObject);

    }

}
