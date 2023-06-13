using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveEffectIcon : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Image statusEffectImage;
    private StatusEffect effect;

    public StatusEffect Effect { get => effect; }

    public void SetUp(StatusEffect givenEffect, Sprite sprite)
    {
        effect = givenEffect;
        statusEffectImage.sprite = sprite;
    }

    public void UpdateCouner()
    {
        counter.text = (effect.Duration - effect.Counter).ToString("F0");
    }

}
