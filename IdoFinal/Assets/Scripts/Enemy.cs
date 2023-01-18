using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Animal RefAnimal;
    private TileData currentPos;

    public TileData CurrentPos { get => currentPos; set => currentPos = value; }

    private void Start()
    {
        DamageDealer.SetStats(RefAnimal);
        Damageable.SetStats(RefAnimal);
    }

}
