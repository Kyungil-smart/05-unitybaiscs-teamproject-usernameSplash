using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float mMoveSpeed = 5f;
    [SerializeField] private float mRotateSpeed = 720f; // deg/sec
    [SerializeField] private bool mFaceMoveDirection = true;

    private bool mClampToBounds = true;
    private Vector2 mBounds = new(-8f, 8f);

    [Header("Physics")]
    [SerializeField] private Rigidbody mRb;

    private Vector3 mDesiredVelocity;
    private Vector3 mKnockbackVel;
    private float mKnockbackDecay = 18f;

    public Vector3 ForwardXZ
    {
        get
        {
            Vector3 f = transform.forward;
            f.y = 0f;
            return f.sqrMagnitude < 0.0001f ? Vector3.forward : f.normalized;
        }
    }

    private void Reset()
    {
        mRb = GetComponent<Rigidbody>();
    }

    public void SetMoveInput(Vector2 input)
    {
        // input: x=좌우(X), y=상하(Z)
        Vector3 dir = new Vector3(input.x, 0f, input.y);
        dir = Vector3.ClampMagnitude(dir, 1f);

        mDesiredVelocity = dir * mMoveSpeed;

        if (mFaceMoveDirection && dir.sqrMagnitude > 0.0001f)
        {
            RotateTowards(dir);
        }
    }

    public void StopMove()
    {
        mDesiredVelocity = Vector3.zero;
    }

    public void AddKnockback(Vector3 kb)
    {
        kb.y = 0f;
        mKnockbackVel += kb;
    }

    private void RotateTowards(Vector3 dir)
    {
        Quaternion target = Quaternion.LookRotation(dir, Vector3.up);
        mRb.rotation = target;
        //transform.rotation = target;//Quaternion.RotateTowards(transform.rotation, target, mRotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // 넉백 감쇠
        mKnockbackVel = Vector3.Lerp(mKnockbackVel, Vector3.zero, mKnockbackDecay * Time.fixedDeltaTime);

        Vector3 vel = mDesiredVelocity + mKnockbackVel;

        if (mRb != null)
        {
            if (mRb.isKinematic)    // kinematic인 경우 넉백 포지션을 직접 지정해야 함. 
            {
                mRb.MovePosition(mRb.position + new Vector3(vel.x, 0f, vel.z) * Time.fixedDeltaTime);
            }
            else
            {
                mRb.angularVelocity = Vector3.zero;
                mRb.velocity = new Vector3(vel.x, mRb.velocity.y, vel.z);
            }
        }
        else
        {
            transform.position += vel * Time.fixedDeltaTime;
        }

        if (mClampToBounds)
        {
            Vector3 p = transform.position;
            p.x = Mathf.Clamp(p.x, mBounds.x, mBounds.y);
            p.z = Mathf.Clamp(p.z, mBounds.x, mBounds.y);
            transform.position = p;
        }
    }
}
