using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Animal refAnimal;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private Transform gfx;
    private TileData currentPos;
    public TileData CurrentPos { get => currentPos; set => currentPos = value; }
    public Animal RefAnimal { get => refAnimal; }
    public EnemyAttackHandler AttackHandler { get => attackHandler; }

    public void SetUpEnemy(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        DamageDealer.SetStats(refAnimal);
        Damageable.SetStats(refAnimal);
        attackHandler.SetUp(this);
        CreateModel();
    }

    private void CreateModel()
    {
        Instantiate(refAnimal.AnimalModel, gfx);
    }

}
