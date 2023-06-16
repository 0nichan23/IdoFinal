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

  

    public Animal RefAnimal { get => refAnimal; }
    public EnemyAttackHandler AttackHandler { get => attackHandler; }

    public override LookDirections LookingTowards => movement.LookingTowards;

    public override AttackCounter Counter => attackHandler.AttackCounter;
    public override float AttackSpeed => attackHandler.AttackSpeed;
    public Transform Gfx { get => gfx; }
    public EnemyMovement Movement { get => movement; }
    public int DetectionRange { get => detectionRange; }
    public AnimationHandler Anim { get => anim; }
    public BaseStateHandler StateHandler { get => stateHandler; }

    public void SetUpEnemy(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        DamageDealer.SetStats(refAnimal, this);
        Damageable.SetStats(refAnimal, this);
        movement.CacheEnemy(this);
        attackHandler.SetUp(this);
        Effectable.CahceOwner(this);
        CreateModel();
        refAnimal.Passive.SubscribePassive(this);
        charger.OnStartCharge.AddListener(() => attackHandler.Charging = true);
        charger.OnEndCharge.AddListener(() => attackHandler.Charging = false);
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

    public override void FireProjectile(AnimalAttack attack)
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
}
