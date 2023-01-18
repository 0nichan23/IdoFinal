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


            if (GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.ComparePositons(tile.GetPos))
            {
                GameManager.Instance.PlayerWrapper.Damageable.GetHit(givenAttack, dealer);
            }

            foreach (var enemy in currentLevel.Enemies)
            {
                if (enemy.CurrentPos.ComparePositons(tile.GetPos))
                {
                    enemy.Damageable.GetHit(givenAttack, dealer);
                }
            }

            tile.Overly.gameObject.SetActive(true);
            tile.Overly.DamageColor();
            //loop over all enemies, + player
            //comapre positoins witht the targeted position
            //deal damage if its the same.

        }
    }

}
