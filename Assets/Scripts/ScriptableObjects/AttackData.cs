using UnityEngine;
using System.Collections.Generic;

public enum AttackType
{
    Physical,
    Fire,
    Water,
    Grass,
    Ice,
    Lightning,
    Poison,
}

// Convert AttackType to string for display purposes
public static class AttackTypeExtensions
{
    public static string ToDisplayString(this AttackType type)
    {
        return type switch
        {
            AttackType.Physical => "Physical",
            AttackType.Fire => "Fire",
            AttackType.Ice => "Ice",
            AttackType.Lightning => "Lightning",
            AttackType.Poison => "Poison",
            AttackType.Water => "Water",
            AttackType.Grass => "Grass",
            _ => "Unknown"
        };
    }
}

public static class TypeEffectiveness
{
    public static readonly Dictionary<AttackType, Dictionary<AttackType, float>> Chart
        = new Dictionary<AttackType, Dictionary<AttackType, float>>
        {
            [AttackType.Physical] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 1.0f,
                [AttackType.Ice] = 1.0f,
                [AttackType.Lightning] = 1.0f,
                [AttackType.Poison] = 1.0f,
                [AttackType.Water] = 1.0f,
                [AttackType.Grass] = 1.0f,
            },
            [AttackType.Fire] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 0.5f,
                [AttackType.Ice] = 2.0f,
                [AttackType.Lightning] = 1.0f,
                [AttackType.Poison] = 1.0f,
                [AttackType.Water] = 0.5f,
                [AttackType.Grass] = 2.0f,
            },
            [AttackType.Ice] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 0.5f,
                [AttackType.Ice] = 0.5f,
                [AttackType.Lightning] = 1.0f,
                [AttackType.Poison] = 1.0f,
                [AttackType.Water] = 2.0f,
                [AttackType.Grass] = 1.0f,
            },
            [AttackType.Lightning] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 1.0f,
                [AttackType.Ice] = 1.0f,
                [AttackType.Lightning] = 0.5f,
                [AttackType.Poison] = 1.0f,
                [AttackType.Water] = 2.0f,
                [AttackType.Grass] = 1.0f,
            },
            [AttackType.Poison] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 1.0f,
                [AttackType.Ice] = 1.0f,
                [AttackType.Lightning] = 1.0f,
                [AttackType.Poison] = 0.5f,
                [AttackType.Water] = 1.0f,
                [AttackType.Grass] = 2.0f,
            },
            [AttackType.Water] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 2.0f,
                [AttackType.Ice] = 1.0f,
                [AttackType.Lightning] = 1.0f,
                [AttackType.Poison] = 1.0f,
                [AttackType.Water] = 0.5f,
                [AttackType.Grass] = 0.5f,
            },
            [AttackType.Grass] = new Dictionary<AttackType, float>
            {
                [AttackType.Physical] = 1.0f,
                [AttackType.Fire] = 0.5f,
                [AttackType.Ice] = 1.0f,
                [AttackType.Lightning] = 1.0f,
                [AttackType.Poison] = 2.0f,
                [AttackType.Water] = 2.0f,
                [AttackType.Grass] = 0.5f,
            }
        
    };
}

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public string attackName = "Basic Attack";
    public string description = "A basic attack that deals damage.";

    public float power = 20.0f;
    public float manaCost = 20.0f;
    public float critChance = 0.15f;
    public AttackType type = AttackType.Physical;
}
