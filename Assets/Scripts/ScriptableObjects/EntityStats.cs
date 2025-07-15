using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Scriptable Objects/EntityStats")]
public class EntityStats : ScriptableObject
{
    public float maxHealth = 100.0f;
    public float maxMana = 100.0f;
    public float attackPower = 1.0f;
    public float defense = 5.0f;
}
