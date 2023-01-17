using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Sheet", menuName = "Stat Sheet")]
public class StatSheet : ScriptableObject
{
    //stats only matter for the active animal
    [SerializeField] private float maxHp; //max hp 
    [SerializeField, Range(1, 10)] private int power; //effects base damage dealt
    [SerializeField, Range(1, 10)] private int toughness; //effects damage taken
    [SerializeField, Range(1, 10)] private int instinct; //crit chance and accuracy
    public float MaxHp { get => maxHp; }
    public int Power { get => power; }
    public int Defense { get => toughness; }
    public int Instinct { get => instinct; }
}
