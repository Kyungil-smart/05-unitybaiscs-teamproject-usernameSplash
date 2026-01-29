using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterMove moveController;
    [SerializeField] private PlayerCombat combatController;
    [SerializeField] private CharacterHealth healthController;
    [SerializeField] private Animator animator;

    private bool mbIsAttacking = false;

    private void Reset()
    {
        moveController = GetComponent<CharacterMove>();
        combatController = GetComponent<PlayerCombat>();
        healthController = GetComponent<CharacterHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!healthController.IsAlive || healthController.IsStunned)
        {
            moveController.StopMove();
            return;
        }

        if (mbIsAttacking)
        {
            moveController.StopMove();
            return;
        }

        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveController.SetMoveInput(moveDir);

        animator.SetFloat("Speed", moveDir.magnitude);

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (combatController.TryAttack("MeleeAttackThrust"))
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("AttackID", 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (combatController.TryAttack("MeleeAttackSweep"))
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("AttackID", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (combatController.TryAttack("MeleeAttackSpin"))
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("AttackID", 2);
            }
        }
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
