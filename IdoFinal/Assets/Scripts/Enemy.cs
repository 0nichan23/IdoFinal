using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Animal refAnimal;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private int detectionRange;
    public Animal RefAnimal { get => refAnimal; }
    public EnemyAttackHandler AttackHandler { get => attackHandler; }

    public override LookDirections LookingTowards => movement.LookingTowards;

    public Transform Gfx { get => gfx; }
    public EnemyMovement Movement { get => movement; }
    public int DetectionRange { get => detectionRange; }

    public void SetUpEnemy(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        DamageDealer.SetStats(refAnimal);
        Damageable.SetStats(refAnimal);
        movement.CacheEnemy(this);
        attackHandler.SetUp(this);
        CreateModel();
    }

    private void CreateModel()
    {
        Instantiate(refAnimal.AnimalModel, gfx);
    }

}
