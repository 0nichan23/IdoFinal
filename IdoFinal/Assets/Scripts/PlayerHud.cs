using TMPro;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    [Header("Stat Sheet")]
    [SerializeField] private GameObject sheet;
    public TextMeshProUGUI attackDamage;
    public TextMeshProUGUI critChance;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI critDamage;
    public TextMeshProUGUI damageReduction;
    public TextMeshProUGUI attackCoolDown;
    public TextMeshProUGUI armorPen;
    public TextMeshProUGUI hitChance;
    public TextMeshProUGUI dodgeChance;


    [Header("Status Effects Bar")]
    public ActiveEffectsBar EffectsBar;

    [Header("Team")]
    public TeamPanel TeamPanel;
    public void ToggleCharacterScreen()
    {
        sheet.SetActive(!sheet.activeSelf);
        GameManager.Instance.PlayerWrapper.UpdatePlayerHud();
    }


}
