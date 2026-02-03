using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float MaxHP = 0.0f;
    [SerializeField] private CharacterMove moveController;
    [SerializeField] private Animator animator;
    [SerializeField] private bool IsInvulnerable = false;

    [SerializeField] private float HP;
    private float mStunTime;

    public bool IsAlive => HP > 0f;
    public bool IsStunned => Time.time < mStunTime;

    public event Action OnDied;
    private bool mDeadCalled;

    private void Reset()
    {
        MaxHP = 0f;

        moveController = GetComponent<CharacterMove>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        HP = MaxHP;

        if (moveController == null)
        {
            moveController = GetComponent<CharacterMove>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void TakeHit(in SHitInfo hit)
    {
        if (!IsAlive || IsInvulnerable)
        {
            return;
        }

        HP -= hit.Damage;
        mStunTime = Mathf.Max(mStunTime, Time.time + hit.StunSec);

        Debug.Log($"{gameObject.name} take hit by {hit.Attacker.name}, Time : {Time.realtimeSinceStartup}");

        if (moveController != null)
        {
            moveController.StopMove();
            moveController.AddKnockback(hit.Knockback);
        }

        if (HP <= 0f)
        {
            HP = 0f;
            if (!mDeadCalled)
            {
                mDeadCalled = true;

                if (animator != null)
                {
                    Debug.Log($"{gameObject.name} is Dead");
                    animator.SetTrigger("Dead");
                }
                OnDied?.Invoke();
            }
        }

        else if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
    }
}