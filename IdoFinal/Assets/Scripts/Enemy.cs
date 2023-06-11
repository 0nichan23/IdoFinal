using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Animal refAnimal;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private int detectionRange;
    [SerializeField] private AnimationHandler anim;
    public Animal RefAnimal { get => refAnimal; }
    public EnemyAttackHandler AttackHandler { get => attackHandler; }

    public override LookDirections LookingTowards => movement.LookingTowards;

    public Transform Gfx { get => gfx; }
    public EnemyMovement Movement { get => movement; }
    public int DetectionRange { get => detectionRange; }
    public AnimationHandler Anim { get => anim; }

    public void SetUpEnemy(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        DamageDealer.SetStats(refAnimal, this);
        Damageable.SetStats(refAnimal, this);
        movement.CacheEnemy(this);
        attackHandler.SetUp(this);
        Effectable.CahceOwner(this);
        CreateModel();
    }

    private void CreateModel()
    {
        GameObject model = Instantiate(refAnimal.AnimalModel, gfx);
        anim.AddAnims(model.transform);
    }

}
