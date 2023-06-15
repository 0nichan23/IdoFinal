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

    public override LookDirections LookingTowards { get => playerMovement.LookingTowards; }
    public override AttackCounter Counter => attackHandler.AttackCounter;
    public override float AttackSpeed => attackHandler.AttackSpeed;
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
    }

    public void StartGame()
    {
        EndStun();
        SetAnimalStatsOnComps();
        team.OnSwitchActiveAnimal.AddListener(SetAnimalStatsOnComps);
        attackHandler.OnAttackPreformed.AddListener(playerAnimationHandler.AttackAnim);
        attackHandler.CacheDealer(DamageDealer);
        Damageable.CacheEffectable(Effectable);
        Effectable.CahceOwner(this);
        Effectable.OnObtainEffect.AddListener(AddEffectIcon);
        animalInventory.OnAnimalAdded.AddListener(playerHud.TeamPanel.InventoryPanel.AddSlot);
    }

    public void SetAnimalStatsOnComps()
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
        playerHud.EffectsBar.UpdateCounters();
    }

    [ContextMenu("update character stat screen")]
    public void UpdatePlayerHud()
    {
        playerHud.hp.text = (Damageable.CurrentHp).ToString() + "/" + (Damageable.MaxHp).ToString() + " hp";
        playerHud.attackDamage.text = (attackHandler.CurrentAttack.Damage.BaseAmount * DamageDealer.PowerDamageMod).ToString("F0") + " damage";
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

    [ContextMenu("test cleanse")]
    public void BleedPlayer()
    {
        Effectable.AddStatus(new Bleed(5), DamageDealer);
        Effectable.AddStatus(new Poison(5, 20), DamageDealer);
        Effectable.AddStatus(new Stun(3), DamageDealer);
    }

    [ContextMenu("test projectile")]
    public void TestProjectile()
    {
        TileData startingTile = null;
        switch (LookingTowards)
        {
            case LookDirections.UP:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(playerMovement.CurrentTile.GetPos + new Vector3Int(0,0,1));
                break;
            case LookDirections.DOWN:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(playerMovement.CurrentTile.GetPos + new Vector3Int(0, 0, -1));
                break;
            case LookDirections.LEFT:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(playerMovement.CurrentTile.GetPos + new Vector3Int(-1, 0, 0));
                break;
            case LookDirections.RIGHT:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(playerMovement.CurrentTile.GetPos + new Vector3Int(1, 0, 0));
                break;
        }
        if (!ReferenceEquals(startingTile, null))
        {
            Projectile pew = GameManager.Instance.PoolManager.TestProjectilePool.GetPooledObject();
            pew.transform.position = startingTile.GetStandingPos;
            pew.CacheCharacter(this);
            pew.gameObject.SetActive(true);
            pew.Shoot(LookingTowards, startingTile);
        }
    }
}
