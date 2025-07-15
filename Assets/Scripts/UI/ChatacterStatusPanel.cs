using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ChatacterStatusPanel : MonoBehaviour
{
    public TurnBasedEntity target;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text mpText;

    private bool isCurrentlyActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Dictionary<string, float> targetStats = target.GetCurrentStats();

        slider.minValue = 0;
        slider.maxValue = targetStats["maxHealth"];
        slider.value = targetStats["currentHealth"];

        nameText.text = target.name;
        hpText.text = $"HP: {targetStats["currentHealth"].ToString("n1")}";
        mpText.text = $"MP: {targetStats["currentMana"].ToString("n1")}";

        var img = GetComponent<Image>();
        var color = img.color;
        color.a = isCurrentlyActive ? 255f : 100f;
        img.color = color;

    }

    public void SetActive(bool active)
    {
        isCurrentlyActive = active;
    }
}
