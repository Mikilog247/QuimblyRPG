using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TurnBasedEntity : MonoBehaviour
{
    public EntityStats stats;
    public AttackData[] attacks;

    public string animationPrefix = "vic_";

    [HideInInspector] public FightUI fightUI;
    [HideInInspector] public AttackData currentAttack;

    private float currentHealth;
    private float currentMana;

    void Start()
    {
        currentHealth = stats.maxHealth;
        currentMana = stats.maxMana;
    }
    
    float CalculateDamage(AttackData attack, TurnBasedEntity attacker)
    {
        TurnBasedEntity defender = this;

        float typeResistance = GetTypeResistance(attack.type);

        float levelFactor = (2f * attacker.stats.level / 5f) + 2f;
        float attackVsDefense = attacker.stats.attackPower / defender.stats.defense;
        float baseDamage = ((levelFactor * attack.power * attackVsDefense) / 50f) + 2f;

        float randomModifier = Random.Range(0.85f, 1.0f);
        float stab = attacker.stats.type == attack.type ? 1.5f : 1f;

        float finalDamage = Mathf.Floor(baseDamage * randomModifier * stab * typeResistance);
        return Mathf.Max(1f, finalDamage);
    }

    public IEnumerator ReceiveAttack(AttackData attack, TurnBasedEntity opponent)
    {
        bool hasCrit = false;

        float finalDamage = CalculateDamage(attack, opponent);

        float typeResistance = GetTypeResistance(attack.type);

        if (typeResistance != 1.0f && (!hasCrit || typeResistance > 1.0f))
        {
            string effectivenessText = typeResistance > 1.0f
                ? "<color={TextColors.TYPE_EFFECTIVENESS}>It's super effective!</color>"
                : "<color={TextColors.TYPE_EFFECTIVENESS}>It's not very effective...</color>";

            fightUI.Log(effectivenessText);
            finalDamage *= typeResistance;
            yield return new WaitForSeconds(0.8f);
        }

        currentHealth -= finalDamage;
        fightUI.Log($"<color={TextColors.ENTITY_NAME}>{gameObject.name}</color> took <color={TextColors.DAMAGE_NUMBER}>{finalDamage.ToString("n1")}</color> {attack.type} damage! Remaining HP: <color={TextColors.DAMAGE_NUMBER}>{currentHealth.ToString("n1")}</color>");
        yield return new WaitForSeconds(0.8f);

        if (hasCrit)
        {
            fightUI.Log($"<color={TextColors.DAMAGE_NUMBER}>Critical hit!</color>");
            yield return new WaitForSeconds(0.8f);
        }

        if (currentHealth <= 0)
        {
            yield return Die();
        }
    }

    public IEnumerator SendAttack(TurnBasedEntity target)
    {
        if (currentHealth <= 0) yield break;
        Animator animator = GetComponent<Animator>();
        animator.Play(animationPrefix + "attack");

        fightUI.Log($"<color={TextColors.ENTITY_NAME}>{gameObject.name}</color> attacks <color={TextColors.ENTITY_NAME}>{target.gameObject.name}</color> with {currentAttack.attackName}!");

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);

        yield return target.ReceiveAttack(currentAttack, this);
    }

    public IEnumerator Die()
    {
        fightUI.Log($"<color=\"red\">{gameObject.name} has been defeated!</color>");
        Destroy(gameObject);
        yield return new WaitForSeconds(0.8f);
    }

    public Dictionary<string, float> GetCurrentStats()
    {
        Dictionary<string, float> statsDict = new()
        {
            ["currentHealth"] = currentHealth,
            ["maxHealth"] = stats.maxHealth,
            ["currentMana"] = currentMana,
            ["maxMana"] = stats.maxMana
        };

        return statsDict;
    }

    public float GetTypeResistance(AttackType attackingType)
    {
        if (TypeEffectiveness.Chart.TryGetValue(stats.type, out var vsDict) &&
            vsDict.TryGetValue(attackingType, out var resistance))
        {
            return resistance;
        }
        return 1.0f; // default if missing
    }
}
