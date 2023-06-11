using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    [Header("Stat Sheet")]
    [SerializeField] public TextMeshProUGUI attackDamage;
    [SerializeField] public TextMeshProUGUI critChance;
    [SerializeField] public TextMeshProUGUI hp;
    [SerializeField] public TextMeshProUGUI critDamage;
    [SerializeField] public TextMeshProUGUI damageReduction;
    [SerializeField] public TextMeshProUGUI attackCoolDown;
    [SerializeField] public TextMeshProUGUI armorPen;
    [SerializeField] public TextMeshProUGUI hitChance;
    [SerializeField] public TextMeshProUGUI dodgeChance;
    

    public void ToggleCharacterScreen()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        GameManager.Instance.PlayerWrapper.UpdatePlayerHud();
    }


}
