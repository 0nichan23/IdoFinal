using UnityEngine;

[CreateAssetMenu(fileName = "BiomeDrop", menuName = "BiomeDrop")]
public class BiomeDropSO : ScriptableObject
{
    [SerializeField] private Sprite artwork;
    [SerializeField] private Rarity rarity;

    public Sprite Artwork { get => artwork; }
    public Rarity Rarity { get => rarity; }
}
