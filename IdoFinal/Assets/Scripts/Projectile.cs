using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float stepDurationMod;
    [SerializeField] private int maxDistance;
    private AnimalAttack attack;
    private Character emitter;
    private AttackTarget targeter = new AttackTarget();

    public void SetUp(Character givenCharacter, AnimalAttack attack)
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
        targeter.AttackTiles(dir, blastZone.GetPos, attack, emitter.DamageDealer);
        gameObject.SetActive(false);
        //play the blast effect?
    }

    private IEnumerator FlyForward(LookDirections direction, TileData startingTile)
    {
        int tileCounter = 0;
        TileData nextTile = startingTile;
        while (tileCounter < maxDistance)
        {
            if (startingTile.Occupied)
            {
                break;
            }
            nextTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(nextTile.GetPos + GetLookDirVector(direction));


            if (ReferenceEquals(nextTile, null) || nextTile.Occupied)
            {
                break;
            }
            else
            {

                float counter = 0f;
                Vector3 startPos = transform.position;
                Vector3 dest = nextTile.GetStandingPos;
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
            Blast(startingTile, direction);

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
