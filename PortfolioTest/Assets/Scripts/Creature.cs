using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header(" 이름")]
    [SerializeField] protected string _name;
    [Header(" 체력")]
    [SerializeField]
    protected int maxHp = 100;
    [Header(" 공격력")]
    [SerializeField] protected int atk = 10;

    

    protected int currentHp;
    public string Name => _name;
    public int MaxHp => maxHp;
    public int CurrentHp => currentHp;
    public int Atk => atk;

    protected virtual void Awake()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{Name}이(가) {damage}만큼 피해를 입었습니다");
        if (currentHp <= 0)
        {
            Die();
        }


    }

    protected virtual void Die()
    {
        currentHp = 0;
        Debug.Log($"{Name} 사망");
        Destroy(gameObject);

    }

}

