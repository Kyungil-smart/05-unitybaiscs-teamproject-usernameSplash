using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterMove moveController;
    [SerializeField] private PlayerCombat combatController;
    [SerializeField] private CharacterHealth healthController;
    [SerializeField] private Animator animator;

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

        if (animator.GetBool("CanMove") == false)
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
        Debug.Log($"AttackAnimStart, Time : {Time.realtimeSinceStartup}");
        //mbIsAttacking = true;
    }

    public void AnimOnAttackEnd()
    {
        Debug.Log($"AttackAnimEnd, Time : {Time.realtimeSinceStartup}");
        //mbIsAttacking = false;
    }

}
