using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public PlayerStatus player;

    public void TakeReward(MonsterStatus monster)
    {
        player.AddExp(monster.Reward.ExpReward);
        player.AddGold(monster.Reward.Gold);

    }

}
