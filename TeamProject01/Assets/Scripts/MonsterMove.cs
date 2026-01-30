using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Transform player;
    [SerializeField] private CharacterHealth healthController;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        healthController = GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        if (player == null) return;

        if (healthController.IsAlive && !healthController.IsStunned)
        {
            nav.SetDestination(player.position);
        }
    }
}
