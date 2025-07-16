using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEventManager : MonoBehaviour
{
    public Button attackButton;
    public FightData fightData;
    public FightUI fightUI;

    [HideInInspector] public TurnBasedEntity currentAlly;

    void Start()
    {
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        StartCoroutine(TurnLoop());
    }

    void OnAttackButtonClicked()
    {
        fightUI.ShowAttackButtons(currentAlly);
    }

    IEnumerator TurnLoop()
    {
        GameObject allies = GameObject.Find("Allies");
        GameObject enemies = GameObject.Find("Enemies");

        while (true)
        {
            currentAlly = allies.transform.GetChild(0).GetComponent<TurnBasedEntity>();
            // Wait for player to click AttackButton
            fightUI.Log("Waiting for player input...");
            fightUI.playerWantsToAttack = false; 
            yield return new WaitUntil(() => fightUI.playerWantsToAttack);

            fightData.currentRound++;

            // Do player's attack
            if (allies.transform.childCount > 0 && enemies.transform.childCount > 0)
            {
                TurnBasedEntity enemyEntity = enemies.transform.GetChild(0).GetComponent<TurnBasedEntity>();

                currentAlly.fightUI = fightUI;
                enemyEntity.fightUI = fightUI;

                if (currentAlly != null && enemyEntity != null)
                {

                    yield return StartCoroutine(currentAlly.SendAttack(enemyEntity));

                    yield return StartCoroutine(enemyEntity.SendAttack(currentAlly));

                }
            }
        }
    }
}
