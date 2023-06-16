using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animal")]
public class Animal : ScriptableObject
{
    [SerializeField] private AnimalClass animalClass;
    [SerializeField] private Habitat habitat;
    [SerializeField] private StatSheet statSheet;
    [SerializeField] private AnimalAttack attack;
    [SerializeField] private AnimalAttack secondAttack;
    [SerializeField] private AnimalPassive passive;
    [SerializeField] private GameObject animalModel;
    [SerializeField] private Diet diet;
    [SerializeField] private Sprite portrait;
    [SerializeField] private Size size;
    [SerializeField] private Rarity rarity;

    public AnimalClass AnimalClass { get => animalClass; }
    public Habitat Habitat { get => habitat; }
    public StatSheet StatSheet { get => statSheet; }
    public AnimalPassive Passive { get => passive; }
    public AnimalAttack Attack { get => attack; }
    public GameObject AnimalModel { get => animalModel; }
    public Diet Diet { get => diet; }
    public Sprite Portrait { get => portrait; }
    public Size Size { get => size; }
    public Rarity Rarity { get => rarity; }
    public AnimalAttack SecondAttack { get => secondAttack; }
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

public enum Diet
{
    Carnivore,
    Herbivore,
    Omnivore
}

public enum Size
{
    Small,
    Medium,
    Large
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare
}

