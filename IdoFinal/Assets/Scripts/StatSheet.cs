using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Sheet", menuName = "Stat Sheet")]
public class StatSheet : ScriptableObject
{
    //stats only matter for the active animal
    [SerializeField] private float maxHp; //max hp 
    [SerializeField, Range(1, 10)] private int power; //effects base damage dealt - > a bear or elephant are powerful animals
    [SerializeField, Range(1, 10)] private int toughness; //effects damage mitigated -> a rhino or turtle are tough animals
    [SerializeField, Range(1, 10)] private int instinct; //crit chance, accuracy, and critical damage -> cats and birds of pray have amazing killer instinct
    [SerializeField, Range(1, 10)] private int speed; //chance of avoiding attacks and attack speed -> smaller birds and mammels are usually quick
    public float MaxHp { get => maxHp; }
    public int Power { get => power; }
    public int Defense { get => toughness; }
    public int Instinct { get => instinct; }
    public int Speed { get => speed; }
}
