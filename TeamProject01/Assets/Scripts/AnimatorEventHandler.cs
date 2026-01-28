using UnityEngine;

public class AnimatorEventHandler : MonoBehaviour
{
    private PlayerInput mInput;

    private void Awake()
    {
        mInput = GetComponentInParent<PlayerInput>();
    }

    public void AnimOnAttackStart()
    {
        mInput.AnimOnAttackStart();
    }

    public void AnimOnAttackEnd()
    {
        mInput.AnimOnAttackEnd();
    }
}
