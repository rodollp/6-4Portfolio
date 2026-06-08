using UnityEngine;

public class Creture : MonoBehaviour
{
    [Header(" 이름")]
    [SerializeField] protected string _name;
    [Header(" 체력")]
    [SerializeField]
    protected int maxHp = 100;
    [Header(" 공격력")]
    [SerializeField] protected int atk = 10;


    public string Name => _name;
    public int MaxHp => maxHp;
    public int Atk => atk;



    public virtual void TakeDamage(int damage)
    {
        maxHp -= damage;
        Debug.Log($"{Name}이(가) {damage}만큼 피해를 입었습니다");
        if (maxHp <= 0)
        {
            Die();
        }


    }

    protected virtual void Die()
    {
        maxHp = 0;
        Debug.Log($"{Name} 사망");
        Destroy(gameObject);

    }

}

