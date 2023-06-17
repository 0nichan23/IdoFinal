using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBlaster
{
    public void FireProjectile(TileData tile, LookDirections dir, Character character, AnimalAttack attack)
    {
        TileData startingTile = null;
        switch (dir)
        {
            case LookDirections.UP:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tile.GetPos + new Vector3Int(0, 0, 1), character.CurrentTileMap);
                break;
            case LookDirections.DOWN:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tile.GetPos + new Vector3Int(0, 0, -1), character.CurrentTileMap);
                break;
            case LookDirections.LEFT:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tile.GetPos + new Vector3Int(-1, 0, 0), character.CurrentTileMap);
                break;
            case LookDirections.RIGHT:
                startingTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tile.GetPos + new Vector3Int(1, 0, 0), character.CurrentTileMap);
                break;
        }
        if (!ReferenceEquals(startingTile, null))
        {
            Projectile pew = GameManager.Instance.PoolManager.TestProjectilePool.GetPooledObject();
            pew.transform.position = startingTile.GetStandingPos(character.MovementMode);
            pew.SetUp(character, attack);
            pew.gameObject.SetActive(true);
            pew.Shoot(dir, startingTile);
        }
    }

}
