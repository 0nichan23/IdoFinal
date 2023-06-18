using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private TileData currentTile;
    [SerializeField] private float movementMod = 1f;
    private LookDirections lookingTowards;
    private Enemy enemy;

    public TileData CurrentTile { get => currentTile; }
    public LookDirections LookingTowards { get => lookingTowards; }


    public void CacheEnemy(Enemy givenEnemy)
    {
        enemy = givenEnemy;
    }

    public void SetEnemyStartPosition(TileData startTile)
    {
        currentTile = startTile;
        currentTile.SubscribeCharacter(enemy);
        transform.position = currentTile.GetStandingPos(enemy.MovementMode);
    }

    public IEnumerator MoveEnemyTo(TileData givenTile)//adjacent to current tile
    {
        if (!ReferenceEquals(givenTile, null) && !givenTile.Occupied)
        {
            LookTowardsMoveDirection(givenTile);
            UpdateCurrentTile(givenTile);
            yield return StartCoroutine(MoveEnemy(givenTile));
        }
    }

    public void UpdateCurrentTile(TileData givenTile)
    {
        currentTile.UnSubscribeCharacter();
        givenTile.SubscribeCharacter(enemy);
        currentTile = givenTile;
    }

    private IEnumerator MoveEnemy(TileData givenTile)
    {
        float counter = 0;
        Vector3 startPos = transform.position;
        Vector3 dest = givenTile.GetStandingPos(enemy.MovementMode);
        enemy.Anim.StartWalkAnim();
        while (counter < 1)
        {
            Vector3 posLerp = Vector3.Lerp(startPos, dest, counter);
            transform.position = posLerp;
            counter += Time.deltaTime * movementMod;
            yield return new WaitForEndOfFrame();
        }
        enemy.Anim.EndWalkAnim();

    }

    public void LookUp()
    {
        lookingTowards = LookDirections.UP;
        enemy.Gfx.eulerAngles = Vector3.zero;
    }
    public void LookDown()
    {
        lookingTowards = LookDirections.DOWN;
        enemy.Gfx.eulerAngles = new Vector3(0, 180, 0);
    }
    public void LookLeft()
    {
        lookingTowards = LookDirections.LEFT;
        enemy.Gfx.eulerAngles = new Vector3(0, -90, 0);

    }
    public void LookRight()
    {
        lookingTowards = LookDirections.RIGHT;
        enemy.Gfx.eulerAngles = new Vector3(0, 90, 0);
    }

    public void LookTowardsMoveDirection(TileData dest)
    {
        if (dest.GetPos.x > currentTile.GetPos.x)
        {
            LookRight();
        }
        else if (dest.GetPos.x < currentTile.GetPos.x)
        {
            LookLeft();
        }
        else if (dest.GetPos.z > currentTile.GetPos.z)
        {
            LookUp();
        }
        else if (dest.GetPos.z < currentTile.GetPos.z)
        {
            LookDown();
        }
    }

}
