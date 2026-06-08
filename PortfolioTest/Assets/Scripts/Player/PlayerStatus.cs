using UnityEngine;

public class PlayerStatus : Creture
{
    public int level = 1;
    public int exp = 0;
    public int expToNext = 10;

    public void AddExp(int amount)
    {
        exp += amount;

        while (exp >= expToNext)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        exp -= expToNext;
        expToNext += 5;

        Debug.Log("Level Up! " + level);
    }

    protected override void Die()
    {
        Debug.Log("게임 오버");
    }
}
