using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float MaxHP = 0.0f;
    [SerializeField] private CharacterMove moveController;
    [SerializeField] private Animator animator;
    [SerializeField] private bool IsInvulnerable = false;

    [SerializeField] private float mHP;
    private float mStunTime;

    public bool IsAlive => mHP > 0f;
    public bool IsStunned => Time.time < mStunTime;

    private void Reset()
    {
        MaxHP = 0f;

        moveController = GetComponent<CharacterMove>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        mHP = MaxHP;

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

        mHP -= hit.Damage;
        mStunTime = Mathf.Max(mStunTime, Time.time + hit.StunSec);

        if (moveController != null)
        {
            moveController.StopMove();
            moveController.AddKnockback(hit.Knockback);
        }

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (mHP <= 0f)
        {
            mHP = 0f;
            if (animator != null)
            {
                animator.SetTrigger("Dead");
            }
        }
    }
}