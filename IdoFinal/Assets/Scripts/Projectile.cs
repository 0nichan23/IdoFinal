using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float stepDurationMod;
    [SerializeField] private int maxDistance;
    private ProjectileAttack attack;
    private Character emitter;
    private AttackTarget targeter = new AttackTarget();

    public void SetUp(Character givenCharacter, ProjectileAttack attack)
    {
        emitter = givenCharacter;
        this.attack = attack;
    }
    public void Shoot(LookDirections direction, TileData startingTile)
    {
        RotateToDir(direction);
        StartCoroutine(FlyForward(direction, startingTile));
    }

    public void Blast(TileData blastZone, LookDirections dir)
    {
        targeter.AttackTiles(dir, blastZone.GetPos, attack, emitter);
        Explosion exp = GetBlastFromElement(attack.Element);
        exp.transform.position = transform.position;
        exp.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private IEnumerator FlyForward(LookDirections direction, TileData startingTile)
    {
        int tileCounter = 0;
        TileData nextTile = startingTile;
        TileData previousTile = null;
        while (tileCounter < maxDistance)
        {
            if (startingTile.Occupied)
            {
                break;
            }
            previousTile = nextTile;
            nextTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(nextTile.GetPos + GetLookDirVector(direction), emitter.CurrentTileMap);


            if (ReferenceEquals(nextTile, null) || nextTile.Occupied)
            {
                break;
            }
            else
            {
                float counter = 0f;
                Vector3 startPos = transform.position;
                Vector3 dest = nextTile.GetStandingPos(emitter.MovementMode);
                while (counter < 1)
                {
                    counter += Time.deltaTime * stepDurationMod;
                    Vector3 lerpedPos = Vector3.Lerp(startPos, dest, counter);
                    transform.position = lerpedPos;
                    yield return new WaitForEndOfFrame();
                }
                if (nextTile.Occupied)
                {
                    break;
                }
            }
            tileCounter++;
        }

        if (ReferenceEquals(nextTile, null))
        {
            Blast(previousTile, direction);

        }
        else
        {
            Blast(nextTile, direction);
        }

    }

    private Vector3Int GetLookDirVector(LookDirections direction)
    {
        switch (direction)
        {
            case LookDirections.UP:
                return new Vector3Int(0, 0, 1);
            case LookDirections.DOWN:
                return new Vector3Int(0, 0, -1);
            case LookDirections.LEFT:
                return new Vector3Int(-1, 0, 0);
            case LookDirections.RIGHT:
                return new Vector3Int(1, 0, 0);
        }
        return Vector3Int.zero;
    }

    private Explosion GetBlastFromElement(Element element)
    {
        switch (element)
        {
            case Element.Lightning:
                return GameManager.Instance.PoolManager.LightningBlastPool.GetPooledObject();
            case Element.Fire:
                return GameManager.Instance.PoolManager.FireBlastPool.GetPooledObject();
            case Element.Poison:
                return GameManager.Instance.PoolManager.PoisonBlastPool.GetPooledObject();
            case Element.Ice:
                return GameManager.Instance.PoolManager.IceBlastPool.GetPooledObject();
            default:
                return null;
        }
    }
    private void RotateToDir(LookDirections dir)
    {

        switch (dir)
        {
            case LookDirections.UP:
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case LookDirections.DOWN:
                transform.eulerAngles = new Vector3(0, -90, 0);
                break;
            case LookDirections.LEFT:
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case LookDirections.RIGHT:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                break;
        }
    }

}