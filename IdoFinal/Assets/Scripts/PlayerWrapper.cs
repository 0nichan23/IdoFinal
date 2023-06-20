using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AnimationHandler playerAnimationHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private PlayerTeam team;
    [SerializeField] private PlayerAttackHandler attackHandler;
    [SerializeField] private PlayerHud playerHud;
    [SerializeField] private AnimalInventory animalInventory;
    [SerializeField] private AnimalAttack testChargeAttack;
    public override LookDirections LookingTowards { get => playerMovement.LookingTowards; }
    public override AttackCounter Counter => attackHandler.AttackCounter;
    public override float AttackSpeed => attackHandler.AttackSpeed;

    public override TileData CurrentTile => playerMovement.CurrentTile;
    public PlayerMovement PlayerMovement { get => playerMovement; }
    public AnimationHandler PlayerAnimationHandler { get => playerAnimationHandler; }
    public Transform Gfx { get => gfx; }
    public PlayerTeam Team { get => team; }
    public PlayerAttackHandler AttackHandler { get => attackHandler; }
    public AnimalInventory AnimalInventory { get => animalInventory; }
    public PlayerHud PlayerHud { get => playerHud; }

    private void Start()
    {
        CreateExistingAnimalSlots();
        team.OnTeamSet.AddListener(SetAnimalSwitchButtons);
    }

    public void StartGame()
    {
        EndStun();
        PlayerAnimationHandler.CacheOwner(this);
        SetAnimalStatsOnComps();
        team.OnSwitchActiveAnimal.AddListener(SetAnimalStatsOnComps);
        team.OnSwitchActiveAnimal.AddListener(playerHud.ToggleTraversalButtons);
        team.OnSwitchActiveAnimal.AddListener(SwitchAnimalCoolDownStart);
        team.OnSwitchActiveAnimal.AddListener(SetAnimalSwitchButtons);
        attackHandler.OnAttackPreformed.AddListener(playerAnimationHandler.AttackAnim);
        attackHandler.CacheDealer(DamageDealer);
        Damageable.CacheEffectable(Effectable);
        Effectable.CahceOwner(this);
        Effectable.OnObtainEffect.AddListener(AddEffectIcon);
        animalInventory.OnAnimalAdded.AddListener(playerHud.TeamPanel.InventoryPanel.AddSlot);
        Damageable.OnHealGFX.AddListener(UpdateBar);
        Damageable.OnTakeDamageGFX.AddListener(UpdateBar);
        attackHandler.OnAttackPreformed.AddListener(SetAttackIconCD);
        attackHandler.OnAttackChanged.AddListener(SetUpAttackIcon);
        PlayerHud.SwitchIcon.AttackSwitchUp.AddListener(() => attackHandler.CanSwitchAttacks = true);
        attackHandler.OnAttackSwitched.AddListener(() => attackHandler.CanSwitchAttacks = false);
        attackHandler.OnAttackSwitched.AddListener(PlayerHud.SwitchIcon.StartCountDown);
        UpdateBar();
        playerHud.ToggleTraversalButtons();
    }

    public void SetAnimalStatsOnComps()
    {
        Damageable.SetStats(team.ActiveAnimal.Animal, this);
        DamageDealer.SetStats(team.ActiveAnimal.Animal, this);
        attackHandler.SetStats(team.ActiveAnimal.Animal);
        Damageable.Heal(new DamageHandler() { BaseAmount = Damageable.MaxHp });
        UpdatePlayerHud();
    }

    [ContextMenu("update character stat screen")]
    public void UpdatePlayerHud()
    {
        playerHud.hp.text = (Damageable.CurrentHp).ToString() + "/" + (Damageable.MaxHp).ToString() + " hp";
        playerHud.attackDamage.text = (attackHandler.CurrentAttack.Damage * DamageDealer.PowerDamageMod).ToString("F0") + " damage";
        playerHud.critChance.text = (DamageDealer.CritChance * 100).ToString("F0") + "% crit chance";
        playerHud.critDamage.text = (DamageDealer.CritDamage * 100).ToString("F0") + "% crit damage";
        playerHud.dodgeChance.text = (Damageable.DodgeChance * 100).ToString("F0") + "% dodge chance";
        playerHud.damageReduction.text = ((1 - Damageable.DamageReduction) * 100).ToString("F0") + "% damage reduction";
        playerHud.hitChance.text = (DamageDealer.HitChance * 100).ToString("F0") + " % hit chance";
        playerHud.armorPen.text = (DamageDealer.ArmorPenetration * 5).ToString("F0") + "% armor pen";
        playerHud.attackCoolDown.text = (attackHandler.GetAttackCoolDown()).ToString("F1") + " seconds";
    }

    public override void AddAttackSpeed(float amount)
    {
        attackHandler.AddAttackSpeed(amount);
    }

    private void AddEffectIcon(StatusEffect effect, Effectable host)
    {
        playerHud.EffectsBar.AddEffect(effect);
    }

    public override void Stun()
    {
        playerMovement.CanMove = false;
        attackHandler.CanAttack = false;
    }
    public override void EndStun()
    {
        playerMovement.CanMove = true;
        attackHandler.CanAttack = true;
    }

    private void CreateExistingAnimalSlots()
    {
        foreach (var item in animalInventory.CaughtAnimals)
        {
            PlayerHud.TeamPanel.InventoryPanel.AddSlot(item);
        }
    }

    private void UpdateBar()
    {
        playerHud.HealthBar.UpdateBar(Damageable.MaxHp, Damageable.CurrentHp);
    }

    public override void UpdateCurrentTile(TileData current)
    {
        PlayerMovement.UpdateCurrentTile(current);
    }
    public override void FireProjectile(ProjectileAttack attack)
    {
        blaster.FireProjectile(playerMovement.CurrentTile, LookingTowards, this, attack);
    }

    public override void Charge(AnimalAttack attack)
    {
        charger.SetUp(attack, this);
        charger.StartCharging(LookingTowards, PlayerMovement.CurrentTile);
    }

    public void SetAttackIconCD()
    {
        playerHud.AttackIcon.SetUpAttack(attackHandler.GetAttackCoolDown());
    }
    public void SetUpAttackIcon()
    {
        playerHud.AttackIcon.SetNewAttack(attackHandler.CurrentAttack.Artwork);
        PlayerHud.AttackIcon.SetUpAttackZ(AttackHandler.GetAttackCoolDown());
    }

    public override void SetStartTraversal()
    {
        switch (team.ActiveAnimal.Animal.MovementMods[0])
        {
            case MovementMode.Ground:
                SetWalkMode(true);
                break;
            case MovementMode.Water:
                SetSwimMode(true);
                break;
            case MovementMode.Air:
                SetFlightMode(true);
                break;
        }
    }

    private void SwitchAnimalCoolDownStart()
    {
        foreach (var item in playerHud.switchButtons)
        {
            item.StartCoolDown();
        }
    }

    private void SetAnimalSwitchButtons()
    {
        playerHud.switchButtons[0].CacheAnimal(team.ActiveAnimal.Animal);
        playerHud.switchButtons[1].CacheAnimal(team.BackLineAnimals[0].Animal);
        playerHud.switchButtons[2].CacheAnimal(team.BackLineAnimals[1].Animal);
       
    }

    public bool TrySetMovementModeOnSwithAttempt(Animal givenAnimal)
    {
        foreach (var item in givenAnimal.MovementMods)
        {
            if (GameManager.Instance.LevelManager.CurrentLevel.GetMapFromMovementMode(item).Contains(GameManager.Instance.PlayerWrapper.CurrentTile))
            {
                SetMovementModeEnum(item);
                return true;
            }
        }
        return false;
    }


    [ContextMenu("test cleanse")]
    public void BleedPlayer()
    {
        Effectable.AddStatus(new Bleed(5), DamageDealer);
        Effectable.AddStatus(new Poison(5, 20), DamageDealer);
        Effectable.AddStatus(new Stun(3), DamageDealer);
    }


}
