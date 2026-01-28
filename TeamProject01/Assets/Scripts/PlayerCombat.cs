using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    [Header("Data Source")]
    [SerializeField] private PlayerAttackDatabase database;

    [Header("Runtime")]
    [SerializeField] private Transform AttackOrigin;
    [SerializeField] private LayerMask TargetMask;

    private readonly HashSet<int> mHitRoots = new HashSet<int>();

    private readonly Dictionary<string, float> mNextTimeById = new Dictionary<string, float>();

    public bool TryAttack(string attackId)
    {
        if (database == null)
        {
            Debug.LogError("[PlayerCombat] PlayerAttackDatabase is null");
            return false;
        }

        PlayerAttackData data = database.Get(attackId);
        if (data == null)
        {
            Debug.LogError($"[PlayerCombat] PlayerAttackData is null, id : {attackId}");
            return false;
        }

        mNextTimeById.TryGetValue(attackId, out float next);
        if (Time.time < next)
        {
            return false;
        }

        Transform origin = AttackOrigin != null ? AttackOrigin : transform;

        // 캐릭터의 전방 방향으로 공격
        Vector3 forward = origin.forward;
        forward.y = 0.0f;
        if (forward.sqrMagnitude < 0.0001f)
        {
            forward = Vector3.forward;
        }
        forward.Normalize();

        if (data.Type == EPlayerAttackType.Melee)
        {
            ExecuteMelee(origin, forward, data);
        }
        else
        {
            ExecuteRanged(origin, forward, data);
        }

        return true;
    }

    private void ExecuteMelee(Transform origin, Vector3 forward, PlayerAttackData data)
    {
        Vector3 center = origin.position + forward * data.RangeForward;

        Collider[] colliders = data.Shape switch
        {
            EHitScanShape.Sphere => Physics.OverlapSphere(center, data.Radius, TargetMask, QueryTriggerInteraction.Collide),
            EHitScanShape.Box => Physics.OverlapBox(center, data.boxHalfExtents, Quaternion.LookRotation(forward), TargetMask, QueryTriggerInteraction.Collide),
            _ => System.Array.Empty<Collider>()
        };

        Debug.Log($"[PlayerCombat] Execute Melee Attack, Target : {colliders.Length}");

        mHitRoots.Clear();

        foreach (Collider collider in colliders)
        {
            if (collider == null)
            {
                Debug.LogError("[PlayerCombat] ExecuteMelee : Collider is Null");
                continue;
            }

            if (collider.transform.root == transform.root)
            {
                continue;
            }

            IDamageable dmg = collider.GetComponentInParent<IDamageable>();
            if (dmg != null || !dmg.IsAlive)
            {
                continue;
            }

            int rootID = collider.transform.root.GetInstanceID();
            if (!mHitRoots.Add(rootID))
            {
                continue;
            }

            Vector3 destinationDir = collider.transform.position - origin.position;
            destinationDir.y = 0f;

            if (destinationDir.sqrMagnitude < 0.0001f)
            {
                continue;
            }

            if (data.Shape == EHitScanShape.Sphere || Vector3.Angle(forward, destinationDir.normalized) > data.Angle * 0.5f)
            {
                continue;
            }

            SHitInfo hitInfo = new SHitInfo()
            {
                Attacker = gameObject,
                Damage = data.Damage,
                StunSec = data.StunSec,
                Knockback = destinationDir.normalized * data.KnockBackPower
            };

            dmg.TakeHit(hitInfo);
        }
    }

    private void ExecuteRanged(Transform origin, Vector3 forward, PlayerAttackData data)
    {
        // Todo
    }
}