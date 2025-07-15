using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEventManager : MonoBehaviour
{
    public Button attackButton;
    public FightData fightData;
    public FightUI fightUI;

    private bool playerWantsToAttack = false;

    void Start()
    {
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        StartCoroutine(TurnLoop());
    }

    void OnAttackButtonClicked()
    {
        playerWantsToAttack = true;
    }

    IEnumerator TurnLoop()
    {
        GameObject allies = GameObject.Find("Allies");
        GameObject enemies = GameObject.Find("Enemies");

        while (true)
        {
            // Wait for player to click AttackButton
            fightUI.Log("Waiting for player input...");
            playerWantsToAttack = false;  // reset
            yield return new WaitUntil(() => playerWantsToAttack);

            fightData.currentRound++;

            // Do player's attack
            if (allies.transform.childCount > 0 && enemies.transform.childCount > 0)
            {
                TurnBasedEntity allyEntity = allies.transform.GetChild(0).GetComponent<TurnBasedEntity>();
                TurnBasedEntity enemyEntity = enemies.transform.GetChild(0).GetComponent<TurnBasedEntity>();

                allyEntity.fightUI = fightUI;
                enemyEntity.fightUI = fightUI;

                if (allyEntity != null && enemyEntity != null)
                {
                    yield return StartCoroutine(allyEntity.SendAttack(enemyEntity));

                    yield return StartCoroutine(enemyEntity.SendAttack(allyEntity));

                }
            }
        }
    }
}
