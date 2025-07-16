using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAttack", menuName = "Scriptable Objects/DamageAttack")]
public class DamageAttack : AttackData
{
    public override IEnumerator Execute(TurnBasedEntity attacker, TurnBasedEntity target, AttackData data)
    {
        float damageDealt = 0;
        yield return target.ReceiveAttack(data, attacker, (result) => damageDealt = result);
    }
}
