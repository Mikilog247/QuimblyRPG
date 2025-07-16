using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAttackButton : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;
    public TMP_Text statsText;
    public TMP_Text descriptionText;

    [HideInInspector] public AttackData attackData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = attackData.attackName;
        statsText.text = $"Power: {attackData.power}\nMana Cost: {attackData.manaCost}\nType: {AttackTypeExtensions.ToDisplayString(attackData.type)}";
        descriptionText.text = attackData.description;
    }
}
