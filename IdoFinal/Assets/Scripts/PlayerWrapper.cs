using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimationHandler playerAnimationHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private PlayerTeam team;
    [SerializeField] private PlayerAttackHandler attackHandler;
    [SerializeField] private PlayerHud playerHud;

    public override LookDirections LookingTowards { get => playerMovement.LookingTowards;}
    public PlayerMovement PlayerMovement { get => playerMovement; }
    public PlayerAnimationHandler PlayerAnimationHandler { get => playerAnimationHandler; }
    public Transform Gfx { get => gfx; }
    public PlayerTeam Team { get => team; }

    private void Start()
    {
        SetAnimalStatsOnComps();
        team.OnSwitchActiveAnimal.AddListener(SetAnimalStatsOnComps);
        attackHandler.OnAttackPreformed.AddListener(playerAnimationHandler.AttackAnim);
        attackHandler.CacheDealer(DamageDealer);
        Damageable.CacheEffectable(Effectable);
    }

    private void SetAnimalStatsOnComps()
    {
        Damageable.SetStats(team.ActiveAnimal.Animal);
        DamageDealer.SetStats(team.ActiveAnimal.Animal);
        attackHandler.SetStats(team.ActiveAnimal.Animal);
        Damageable.Heal(new DamageHandler() { BaseAmount = Damageable.MaxHp });
        UpdatePlayerHud();
        attackHandler.EquipAttack(team.ActiveAnimal.Animal.Attack);
    }

    [ContextMenu("update character stat screen")]
    private void UpdatePlayerHud()
    {
        playerHud.hp.text = (Damageable.CurrentHp).ToString() + "/" + (Damageable.MaxHp).ToString() + " hp";
        playerHud.attackDamage.text = (attackHandler.CurrentAttack.Damage.BaseAmount * DamageDealer.PowerDamageMod).ToString() + " damage";
        playerHud.critChance.text = (DamageDealer.CritChance).ToString() + " crit chance";
        playerHud.critDamage.text = (DamageDealer.CritDamage).ToString() + " crit damage";
        playerHud.dodgeChance.text = (Damageable.DodgeChance).ToString() + " dodge chance";
        playerHud.damageReduction.text = (1 - Damageable.DamageReduction).ToString() + " damage reduction";
        playerHud.hitChance.text = (DamageDealer.HitChance).ToString() + " hit chance";
        playerHud.armorPen.text = (DamageDealer.ArmorPenetration).ToString() + " armor pen";
        playerHud.attackCoolDown.text = (attackHandler.GetAttackCoolDown()).ToString() + " attack CoolDown";
    }
}
