using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Sheet", menuName = "Stat Sheet")]
public class StatSheet : ScriptableObject
{
    [SerializeField] private float maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int speed;
    public float MaxHp { get => maxHp; }
    public int Attack { get => attack; }
    public int Defense { get => defense; }
    public int Speed { get => speed; }
}
