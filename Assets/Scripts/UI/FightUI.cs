using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using JetBrains.Annotations;

public class FightUI : MonoBehaviour
{
    public int logSize = 8;

    public FightData fightData;
    public TMP_Text RoundText;
    public TMP_Text ConsoleText;
    public GameObject attacksPanel;

    public PlayerAttackButton attackButtonPrefab;

    public List<string> consoleLog;

    [HideInInspector] public bool playerWantsToAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacksPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RoundText.text = $"Turn {fightData.currentRound}";
    }

    public void Log(string text)
    {
        consoleLog.Add(text);

        while (consoleLog.Count > logSize) consoleLog.RemoveAt(0);

        string textToDisplay = "";

        foreach (string logEntry in consoleLog)
        {
            textToDisplay += $"{logEntry}\n";
        }

        ConsoleText.text = textToDisplay;
    }

    public void ShowAttackButtons(TurnBasedEntity entity)
    {

        attacksPanel.SetActive(true);

        foreach (Transform child in attacksPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (AttackData attack in entity.attacks)
        {
            PlayerAttackButton button = Instantiate(attackButtonPrefab, attacksPanel.transform);
            button.attackData = attack;
            button.button.onClick.AddListener(() => OnAttackChosen(entity, attack));
        }
    }

    public void OnAttackChosen(TurnBasedEntity entity, AttackData attack)
    {
        attacksPanel.SetActive(false);
        entity.currentAttack = attack;
        playerWantsToAttack = true;
    }
}
