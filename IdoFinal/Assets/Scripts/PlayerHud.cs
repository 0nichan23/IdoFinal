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

    [Header("Bars")]
    public Bar HealthBar;
    public Bar XpBar;

    [Header("Attack Icon")]
    public AttackIcon AttackIcon;
    public SwitchAttackIcon SwitchIcon;

    [Header("Traversals")]
    public GameObject Flybutton;
    public GameObject SwimButton;
    public GameObject WalkButton;

    [Header("Team Switch Buttons")]
    public AnimalSwitchButton[] switchButtons;

    [Header("CraftingUI")]
    public DropPanel DropPanel;

    public void ToggleCharacterScreen()
    {
        sheet.SetActive(!sheet.activeSelf);
        GameManager.Instance.PlayerWrapper.UpdatePlayerHud();
    }

    public void ToggleTraversalButtons()
    {
        WalkButton.SetActive(false);
        SwimButton.SetActive(false);
        Flybutton.SetActive(false);
        foreach (var item in GameManager.Instance.PlayerWrapper.Team.ActiveAnimal.Animal.MovementMods)
        {
            switch (item)
            {
                case MovementMode.Ground:
                    WalkButton.SetActive(true);
                    break;
                case MovementMode.Water:
                    SwimButton.SetActive(true);
                    break;
                case MovementMode.Air:
                    Flybutton.SetActive(true);
                    break;
            }
        }
    }


}
