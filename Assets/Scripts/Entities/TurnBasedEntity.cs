using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TurnBasedEntity : MonoBehaviour
{
    public EntityStats stats;
    public AttackData currentAttack;

    public string animationPrefix = "vic_";

    [HideInInspector]
    public FightUI fightUI;

    private float currentHealth;
    private float currentMana;

    void Start()
    {
        currentHealth = stats.maxHealth;
        currentMana = stats.maxMana;
    }

    public IEnumerator ReceiveAttack(AttackData attack, TurnBasedEntity opponent)
    {
        bool hasCrit = false;

        float finalDamage = Mathf.Max(1f, (attack.power * opponent.stats.attackPower * Random.Range(0.85f, 1.15f)) - stats.defense);

        if (Random.Range(0f, 1f) < attack.critChance)
        {
            finalDamage = Mathf.RoundToInt(finalDamage * 1.5f);
            hasCrit = true;
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
}
