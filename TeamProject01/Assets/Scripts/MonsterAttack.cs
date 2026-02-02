using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private PlayerCombat combatController;
    [SerializeField] private CharacterHealth healthController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    [SerializeField] private float AttackDistance = 1.5f; // 공격시도 거리

    [Header("Attack ID")]
    [SerializeField] private string Attack1Id;
    [SerializeField] private string Attack2Id;
    [SerializeField] private string Attack3Id;

    [Header("Percent")]
    [SerializeField] private int Attack1Percent = 50;
    [SerializeField] private int Attack2Percent = 30;
    [SerializeField] private int Attack3Percent = 20;

    private bool mbIsAttacking = false;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        combatController = GetComponent<PlayerCombat>();
        healthController = GetComponent<CharacterHealth>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!healthController.IsAlive || healthController.IsStunned)
        {
            nav.isStopped = true;
            return;
        }

        if (mbIsAttacking)
        {
            nav.isStopped = true;
            return;
        }

        if (player == null)
        {
            return;
        }
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > AttackDistance)
        {
            return;
        }

        nav.isStopped = true;

        string selectedAttack = SelectAttackId();

        if (TryExecuteAttack(selectedAttack))
        {
            return;
        }
        if (selectedAttack != Attack1Id && TryExecuteAttack(Attack1Id))
        {
            return;
        }
        if (selectedAttack != Attack2Id && TryExecuteAttack(Attack2Id))
        {
            return;
        }
        if (selectedAttack != Attack3Id && TryExecuteAttack(Attack3Id))
        {
            return;
        }
    }

    private string SelectAttackId()
    {
        int total = Attack1Percent + Attack2Percent + Attack3Percent;
        int r = Random.Range(0, total);

        if (r < Attack1Percent) return Attack1Id;
        else if (r < Attack1Percent + Attack2Percent) return Attack2Id;
        return Attack3Id;
    }

    private bool TryExecuteAttack(string attackId)
    {
        if (!combatController.TryAttack(attackId))
        {
            return false;
        }

        animator.SetTrigger("Attack");
        animator.SetInteger("AttackID", AttackIdToInt(attackId));
        return true;
    }

    private int AttackIdToInt(string id)
    {
        if (id == Attack1Id) return 0;
        if (id == Attack2Id) return 1;
        if (id == Attack3Id) return 2;
        return 0;
    }

    public void AnimOnAttackStart()
    {
        mbIsAttacking = true;
    }

    public void AnimOnAttackEnd()
    {
        mbIsAttacking = false;
    }
}
