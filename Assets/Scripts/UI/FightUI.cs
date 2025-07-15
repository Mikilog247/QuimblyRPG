using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FightUI : MonoBehaviour
{
    public int logSize = 8;

    public FightData fightData;
    public TMP_Text RoundText;
    public TMP_Text ConsoleText;

    public List<string> consoleLog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
