using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AbsorbAttack", menuName = "Scriptable Objects/AbsorbAttack")]
public class AbsorbAttack : AttackData
{
    public float absorbPercentage = 0.2f; // Percentage of damage dealt to heal the attacker

    public override IEnumerator Execute(TurnBasedEntity attacker, TurnBasedEntity target, AttackData data)
    {
        float damageDealt = 0;
        yield return target.ReceiveAttack(data, attacker, (result) => damageDealt = result);
        attacker.AddHealth(damageDealt * absorbPercentage); // Heal for 20% of damage dealt
        yield return new WaitForSeconds(0.8f);
    }
}