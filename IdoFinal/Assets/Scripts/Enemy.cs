using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    [SerializeField] private Animal refAnimal;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private int detectionRange;
    [SerializeField] private AnimationHandler anim;
    [SerializeField] private BaseStateHandler stateHandler;
    [SerializeField] private EnemyPanel panel;

  

    public Animal RefAnimal { get => refAnimal; }
    public EnemyAttackHandler AttackHandler { get => attackHandler; }

    public override LookDirections LookingTowards => movement.LookingTowards;

    public override AttackCounter Counter => attackHandler.AttackCounter;
    public override float AttackSpeed => attackHandler.AttackSpeed;

    public override TileData CurrentTile => movement.CurrentTile;
    public Transform Gfx { get => gfx; }
    public EnemyMovement Movement { get => movement; }
    public int DetectionRange { get => detectionRange; }
    public AnimationHandler Anim { get => anim; }
    public BaseStateHandler StateHandler { get => stateHandler; }
    public EnemyPanel Panel { get => panel; }

    public void SetUpEnemy(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        anim.CacheOwner(this);
        DamageDealer.SetStats(refAnimal, this);
        Damageable.SetStats(refAnimal, this);
        movement.CacheEnemy(this);
        attackHandler.SetUp(this);
        Effectable.CahceOwner(this);
        Damageable.OnDeath.AddListener(EnemyDeath);
        CreateModel();
        refAnimal.Passive.SubscribePassive(this);
        charger.OnStartCharge.AddListener(() => attackHandler.Charging = true);
        charger.OnEndCharge.AddListener(() => attackHandler.Charging = false);
        Effectable.OnObtainEffect.AddListener(AddEffectIcon);
        Damageable.OnTakeDamageGFX.AddListener(UpdateBar);
        Damageable.OnHealGFX.AddListener(UpdateBar);
        panel.HealthBar.SetUp(RefAnimal);
    }

    private void CreateModel()
    {
        GameObject model = Instantiate(refAnimal.AnimalModel, gfx);
        anim.AddAnims(model.transform);
    }

    public override void AddAttackSpeed(float amount)
    {
        attackHandler.AddAttackSpeed(amount);
    }

    public override void Stun()
    {
        stateHandler.Stunned = true;
    }
    public override void EndStun()
    {
        stateHandler.Stunned = false;
    }

    public override void FireProjectile(ProjectileAttack attack)
    {
        blaster.FireProjectile(movement.CurrentTile, LookingTowards, this, attack);
    }
    public override void Charge(AnimalAttack attack)
    {
        charger.SetUp(attack, this);
        charger.StartCharging(LookingTowards, movement.CurrentTile);
    }

    public override void UpdateCurrentTile(TileData current)
    {
        movement.UpdateCurrentTile(current);
    }
    private void AddEffectIcon(StatusEffect effect, Effectable host)
    {
        panel.Bar.AddEffect(effect);
    }

    private void UpdateBar()
    {
        panel.HealthBar.UpdateBar(Damageable.MaxHp, Damageable.CurrentHp);
    }

    private void EnemyDeath()
    {
        CurrentTile.UnSubscribeCharacter();
        gameObject.SetActive(false);
    }
}
