using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Transform player;
    [SerializeField] private CharacterHealth healthController;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthController = GetComponent<CharacterHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (!healthController.IsAlive || healthController.IsStunned)
        {
            nav.isStopped = true;
            animator.SetFloat("Speed", 0f);
            return;
        }

        if (nav.isStopped)
        {
            nav.isStopped = false;
        }

        nav.SetDestination(player.position);
        animator.SetFloat("Speed", nav.velocity.magnitude);
    }
}
