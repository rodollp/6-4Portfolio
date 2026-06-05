using System;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] Transform player;

    [Header("몬스터 눈 위치")]
    [SerializeField] Transform eyePoint;

    [Header("이동 속도")]
    [SerializeField] float moveSpeed = 3f;

    [Header("시야각")]
    [SerializeField] float sightAngle = 60f;

    [Header("인식 범위")]
    [SerializeField] float detectRange = 10f;

    [Header("공격 범위")]
    [SerializeField] float attackRange = 2f;
    float attackTimer = 0;
    float coolDown = 1f;
    private enum MonsterState
    {
        Idle,
        Chase,
        Attack

    }

    MonsterState currentState = MonsterState.Idle;

    private void Update()
    {
        switch(currentState)
        {
            case MonsterState.Idle:
                Idle();
                break;
                case MonsterState.Chase:
                Chase();
                break;
                case MonsterState.Attack: 
                Attack(); 
                break;

        }

    }

    bool CanSeePlayer()
    {
        Vector3 toPlayer =
            (player.position - eyePoint.position).normalized;

        float distance =
            (player.position - transform.position).sqrMagnitude;

        if (distance > detectRange * detectRange)
            return false;

        float dot =
            Vector3.Dot(eyePoint.forward, toPlayer);

        float limitDot =
            Mathf.Cos(sightAngle * 0.5f * Mathf.Deg2Rad);

        return dot >= limitDot;
    }
    bool IsInAttackRange()
    {
        float distance =
            (player.position - transform.position).sqrMagnitude;

        return distance < attackRange * attackRange;
    }
    void MoveToPlayer()
    {
        transform.position +=
            transform.forward *
            moveSpeed *
            Time.deltaTime;
    }
    void LookPlayer()
    {
        Vector3 dir =
            player.position - transform.position;

        dir.y = 0;

        Quaternion targetRot =
            Quaternion.LookRotation(dir);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRot,
                5f * Time.deltaTime
            );
    }
    void Idle()
    {
        if (CanSeePlayer())
        {
            currentState = MonsterState.Chase;
        }
    }
    void Chase()
    {
        LookPlayer();

        if (IsInAttackRange())
        {
            currentState = MonsterState.Attack;
            return;
        }

        MoveToPlayer();

        if (!CanSeePlayer())
        {
            currentState = MonsterState.Idle;
        }
    }
    void Attack()
    {
        
        LookPlayer();

        MonsterAttack();

        if (!IsInAttackRange())
        {
            currentState = MonsterState.Chase;
        }
    }

    void MonsterAttack()
    {

        

        attackTimer += Time.deltaTime;

        

        if (attackTimer >= coolDown)
        {
            attackTimer = 0f;
            Debug.Log("몬스터의 공격!");
        }
    }
    private void OnDrawGizmos()
    {
        if (eyePoint == null)
            return;

        // =========================
        // 인식 범위
        // =========================

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // =========================
        // 공격 범위
        // =========================

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // =========================
        // 시야각 좌/우 선
        // =========================

        Vector3 leftDir =
            Quaternion.Euler(0, -sightAngle * 0.5f, 0)
            * eyePoint.forward;

        Vector3 rightDir =
            Quaternion.Euler(0, sightAngle * 0.5f, 0)
            * eyePoint.forward;

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(
            eyePoint.position,
            eyePoint.position + leftDir * detectRange
        );

        Gizmos.DrawLine(
            eyePoint.position,
            eyePoint.position + rightDir * detectRange
        );

        // =========================
        // 플레이어 방향 확인
        // =========================

        if (player != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(
                eyePoint.position,
                player.position
            );
        }

        // =========================
        // 몬스터 정면
        // =========================

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(
            eyePoint.position,
            eyePoint.position + eyePoint.forward * detectRange
        );
    }
}