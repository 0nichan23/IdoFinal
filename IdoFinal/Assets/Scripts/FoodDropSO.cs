using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food")]
public class FoodDropSO : BasicDropSO
{
    [SerializeField] private Sprite artwork;
    [SerializeField] private Diet diet;
    [SerializeField] private Size size;
    public Sprite Artwork { get => artwork; }
    public Diet Diet { get => diet; }
    public Size Size { get => size; }
}
