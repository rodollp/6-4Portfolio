using UnityEngine;

public class PlayerStatus : Creature
{
    [SerializeField] private int level = 1;
    [SerializeField] private int exp = 0;
    [SerializeField] private int expToNext = 10;
    [SerializeField] private int money = 0;

    public int Level => level;
    public int Exp => exp;
    public int ExpToNext => expToNext;

    public int Money => money;
    protected override void Awake()
    {
        base.Awake();
    }

    public void AddExp(int amount)
    {
        exp += amount;

        while (exp >= expToNext)
        {
            LevelUp();
        }
    }

    public void AddGold(int amount)
    {
        money += amount;
    }

    void LevelUp()
    {
        level++;
        exp -= expToNext;
        expToNext += 5;

        maxHp += 3;
        currentHp += 3;
        atk += 3;

        Debug.Log("Level Up! " + level);
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp < 0)
            currentHp = 0;

        Debug.Log($"플레이어가 {damage} 피해를 받음 / 남은 HP: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Debug.Log("게임 오버");
    }
}
