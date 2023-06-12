using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AnimationHandler playerAnimationHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private PlayerTeam team;
    [SerializeField] private PlayerAttackHandler attackHandler;
    [SerializeField] private PlayerHud playerHud;

    public override LookDirections LookingTowards { get => playerMovement.LookingTowards; }
    public override AttackCounter Counter => attackHandler.AttackCounter;
    public override float AttackSpeed => attackHandler.AttackSpeed;
    public PlayerMovement PlayerMovement { get => playerMovement; }
    public AnimationHandler PlayerAnimationHandler { get => playerAnimationHandler; }
    public Transform Gfx { get => gfx; }
    public PlayerTeam Team { get => team; }
    public PlayerAttackHandler AttackHandler { get => attackHandler;}


    private void Start()
    {
        SetAnimalStatsOnComps();
        team.OnSwitchActiveAnimal.AddListener(SetAnimalStatsOnComps);
        attackHandler.OnAttackPreformed.AddListener(playerAnimationHandler.AttackAnim);
        attackHandler.CacheDealer(DamageDealer);
        Damageable.CacheEffectable(Effectable);
        Effectable.CahceOwner(this);
        DamageDealer.OnDealDamageFinal.AddListener(DamagePopupTest);
    }

    private void SetAnimalStatsOnComps()
    {
        Damageable.SetStats(team.ActiveAnimal.Animal, this);
        DamageDealer.SetStats(team.ActiveAnimal.Animal, this);
        attackHandler.SetStats(team.ActiveAnimal.Animal);
        Damageable.Heal(new DamageHandler() { BaseAmount = Damageable.MaxHp });
        UpdatePlayerHud();
        attackHandler.EquipAttack(team.ActiveAnimal.Animal.Attack);
    }

    private void FixedUpdate()
    {
        UpdatePlayerHud();
    }

    [ContextMenu("update character stat screen")]
    public void UpdatePlayerHud()
    {
        playerHud.hp.text = (Damageable.CurrentHp).ToString() + "/" + (Damageable.MaxHp).ToString() + " hp";
        playerHud.attackDamage.text = (attackHandler.CurrentAttack.Damage.BaseAmount * DamageDealer.PowerDamageMod).ToString("F0") + " damage";
        playerHud.critChance.text = (DamageDealer.CritChance).ToString("F1") + " crit chance";
        playerHud.critDamage.text = (DamageDealer.CritDamage).ToString("F1") + " crit damage";
        playerHud.dodgeChance.text = (Damageable.DodgeChance).ToString("F1") + " dodge chance";
        playerHud.damageReduction.text = (1 - Damageable.DamageReduction).ToString("F1") + " damage reduction";
        playerHud.hitChance.text = (DamageDealer.HitChance).ToString("F1") + " hit chance";
        playerHud.armorPen.text = (DamageDealer.ArmorPenetration).ToString("F1") + " armor pen";
        playerHud.attackCoolDown.text = (attackHandler.GetAttackCoolDown()).ToString("F1") + " attack CoolDown";
    }

    public override void AddAttackSpeed(float amount)
    {
        attackHandler.AddAttackSpeed(amount);
    }

    private void DamagePopupTest(AnimalAttack attack)
    {
        GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, attack.Damage.CalcFinalDamageMult());
    }
  


    [ContextMenu("test cleanse")]
    public void BleedPlayer()
    {
        Effectable.AddStatus(new Bleed(5), DamageDealer);
    }
}
