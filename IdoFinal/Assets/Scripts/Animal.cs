using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private AnimalClass animalClass;
    [SerializeField] private Habitat habitat;
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Effectable effectable;
    [SerializeField] private StatSheet statSheet;
    [SerializeField] private AnimalAttack attack;
    [SerializeField] private AnimalPassive passive;
    //animal stats -> this needs to hold hp, base attack, defense, speed?
    //animal passive -> this needs to subscribe to an according event/ change some stat
    //animal attack -> this is the attack itself, has additional effects and base damage
    public AnimalClass AnimalClass { get => animalClass; }
    public Habitat Habitat { get => habitat; }
    public Damageable Damageable { get => damageable; }
    public StatSheet StatSheet { get => statSheet; }
    public AnimalPassive Passive { get => passive; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Effectable Effectable { get => effectable; }

    private void Start()
    {
        Damageable.SetStats(this);
    }

}

public enum AnimalClass
{
    Mammel, 
    Bird,
    Fish,
    Reptile,
    Amphibian
}

public enum Habitat
{
    Desert,
    Forest,
    Jungle,
    FreshWater,
    SaltWater,
    Plains,
    Mountain,
    Arctic,
    Domestic
}

