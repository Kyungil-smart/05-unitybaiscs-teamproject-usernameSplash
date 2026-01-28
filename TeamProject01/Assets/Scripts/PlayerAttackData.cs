using UnityEngine;

public enum EPlayerAttackType
{
    Melee,
    Ranged
}

public enum EHitScanShape
{
    Box,    // Âî¸£±â
    Sphere  // ÈÛ¾µ±â
}

[CreateAssetMenu(menuName = "Game/Combat/AttackData")]
public class PlayerAttackData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] public string ID;
    [SerializeField] public EPlayerAttackType Type;

    [Header("Common")]
    [SerializeField] public float Damage = 0.0f;
    [SerializeField] public float StunSec = 0.0f;
    [SerializeField] public float CoolDown = 0.0f;
    [SerializeField] public float KnockBackPower = 0.0f;

    [Header("Melee")]
    [SerializeField] public EHitScanShape Shape;
    [SerializeField] public float RangeForward = 0.0f;
    [SerializeField] public float Angle = 0.0f;
    [SerializeField] public float Radius = 0.0f;
    [SerializeField] public Vector3 boxHalfExtents = new Vector3();

    [Header("Ranged")]
    //[SerializeField] public Projectile ProjectilePrefab;
    [SerializeField] public float ProjectileSpeed = 0.0f;
    [SerializeField] public float projectileLifeTime = 0.0f;
}