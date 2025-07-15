using UnityEngine;

public enum AttackType
{
    Physical,
    Fire,
    Ice,
    Lightning,
    Poison
}

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public string attackName = "Basic Attack";

    public float power = 20.0f;
    public float manaCost = 20.0f;
    public float critChance = 0.15f;
    public AttackType type = AttackType.Physical;
}
