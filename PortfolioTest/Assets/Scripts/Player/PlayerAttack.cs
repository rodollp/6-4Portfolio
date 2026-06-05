using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("타겟 설정 : 몬스터")]
    [SerializeField] Transform target;
    [Header("공격 범위 설정")]
    [SerializeField] private float attackRange = 2f;
    [Header("밀치는 힘")]
    [SerializeField] private float attackForce = 5f;

    

    private void Update()
    {
        if(Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Attack();

            }
        }

        


    }

    private void Attack()
    {
        if (!IsInRangeTarget()) return;

        KnockBack();
        
    }

    private void KnockBack()
    {
        // 몬스터가 Rigidbody를 가지게 만들고 
        Rigidbody rb = target.GetComponent<Rigidbody>();

        if(rb != null )
        {
            // 플레이어 방향 >> 몬스터 방향 의 정규화
            Vector3 dir = (target.position - transform.position).normalized;
            // 물리력 만큼 뒤로 밀림
            Vector3 push = dir * attackForce;
            //방향을 살짝 위로
            push.y = 1f;

            rb.AddForce(push,ForceMode.Impulse);    
        }

    }

    // 플레이어의 공격 범위 내에 있는지 확인
    private bool IsInRangeTarget()
    {
        if (target == null)
        {
            return false;
        }

        //플레이어와 몬스터의 거리를 sqrMagnitude를 이용하여 제곱 거리를 확인
        Vector3 distance = transform.position - target.position;
        float inRange = distance.sqrMagnitude;

        // 제곱의 거리가 범위 안에 있으면 True
        return inRange <= attackRange*attackRange;

    }

    


}