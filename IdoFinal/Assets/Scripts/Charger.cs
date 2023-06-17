using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Charger : MonoBehaviour
{
    [SerializeField] private int maxDistance;
    [SerializeField] private float stepDurationMod;
    private AttackTarget targeter = new AttackTarget();
    private AnimalAttack attack;
    private Character emitter;
    public UnityEvent OnStartCharge;
    public UnityEvent OnEndCharge;

    public int MaxDistance { get => maxDistance; }

    public void SetUp(AnimalAttack attack, Character character)
    {
        this.attack = attack;
        this.emitter = character;
    }

    public void StartCharging(LookDirections direction, TileData startingTile)
    {
        TileData startTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(startingTile.GetPos + GetLookDirVector(direction), emitter.CurrentTileMap);
        if (!ReferenceEquals(startTile, null))
        {
            StartCoroutine(ChargeForward(direction, startTile));
        }
    }

    private IEnumerator ChargeForward(LookDirections direction, TileData startingTile)
    {
        int tileCounter = 0;
        TileData nextTile = startingTile;
        TileData previousTile = null;
        while (tileCounter < maxDistance)
        {
            OnStartCharge?.Invoke();
            if (startingTile.Occupied)
            {
                break;
            }
            previousTile = nextTile;
            nextTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(nextTile.GetPos + GetLookDirVector(direction), emitter.CurrentTileMap);
            if (ReferenceEquals(nextTile, null) || nextTile.Occupied)
            {
                nextTile = previousTile;
                break;
            }
            else
            {
                emitter.UpdateCurrentTile(nextTile);
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
        OnEndCharge?.Invoke();

    }

    public void Blast(TileData blastZone, LookDirections dir)
    {
        targeter.AttackTiles(dir, blastZone.GetPos, attack, emitter);
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
}


