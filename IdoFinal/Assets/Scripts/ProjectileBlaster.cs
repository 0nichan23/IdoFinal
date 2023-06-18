using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBlaster
{
    public void FireProjectile(TileData tile, LookDirections dir, Character character, ProjectileAttack attack)
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
            Projectile pew = GetProjectileType(attack.Element);
            pew.transform.position = startingTile.GetStandingPos(character.MovementMode);
            pew.SetUp(character, attack);
            pew.gameObject.SetActive(true);
            pew.Shoot(dir, startingTile);
        }
    }

    private Projectile GetProjectileType(Element element)
    {
        switch (element)
        {
            case Element.Lightning:
                return GameManager.Instance.PoolManager.LightningProjectilePool.GetPooledObject();
            case Element.Fire:
                return GameManager.Instance.PoolManager.FireProjectilePool.GetPooledObject();
            case Element.Poison:
                return GameManager.Instance.PoolManager.PoisonProjectilePool.GetPooledObject();
            case Element.Ice:
                return GameManager.Instance.PoolManager.IceProjectilePool.GetPooledObject();
            default:
                return null;
        }
    }

}
