using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel;

    public Level CurrentLevel { get => currentLevel; }

    public void DealDamageOnTiles(List<Vector3Int> givenPositions, AnimalAttack givenAttack, DamageDealer dealer = null)
    {
        foreach (var item in givenPositions)
        {
            TileData tile = currentLevel.GetTile(item);
            if (ReferenceEquals(tile, null))
            {
                continue;
            }

            tile.HitTile(givenAttack, dealer);

            tile.Overly.gameObject.SetActive(true);
            tile.Overly.DamageColor();
        }
    }



}
