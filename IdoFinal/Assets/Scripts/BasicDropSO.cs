using UnityEngine;

public class BasicDropSO : ScriptableObject
{
    [SerializeField] private Sprite artwork;

    public Sprite Artwork { get => artwork; }
}
