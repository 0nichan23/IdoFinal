using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animal")]
public class Animal : ScriptableObject
{
    [SerializeField] private AnimalClass animalClass;
    [SerializeField] private Habitat habitat;
    [SerializeField] private StatSheet statSheet;
    [SerializeField] private AnimalAttack attack;
    [SerializeField] private AnimalPassive passive;
    [SerializeField] private GameObject animalModel;
    //animal stats -> this needs to hold hp, base attack, defense, speed?
    //animal passive -> this needs to subscribe to an according event/ change some stat
    //animal attack -> this is the attack itself, has additional effects and base damage
    public AnimalClass AnimalClass { get => animalClass; }
    public Habitat Habitat { get => habitat; }
    public StatSheet StatSheet { get => statSheet; }
    public AnimalPassive Passive { get => passive; }
    public AnimalAttack Attack { get => attack; }
    public GameObject AnimalModel { get => animalModel; }
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

