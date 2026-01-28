using UnityEngine;

public struct SHitInfo
{
    public GameObject Attacker { get; set; }
    public float Damage { get; set; }
    public Vector3 Knockback { get; set; } // Use only X and Z
    public float StunSec { get; set; }
}

public interface IDamageable
{
    bool IsAlive { get; }

    void TakeHit(in SHitInfo hit);
}