using UnityEngine;

[CreateAssetMenu(fileName = "BiomeDrop", menuName = "BiomeDrop")]
public class BiomeDropSO : BasicDropSO
{
    [SerializeField] private Rarity rarity;
    [SerializeField] private Habitat habitat;

    public Rarity Rarity { get => rarity; }
    public Habitat Habitat { get => habitat; }
}
