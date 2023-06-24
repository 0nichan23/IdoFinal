using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Effectable effectable;
    [SerializeField] private CharacterLevel level;
    public UnityEvent<Level, Character> OnEnteredLevel;
    public UnityEvent<Level, Character> OnExitLevel;
    private LookDirections lookingTowards;
    private AttackCounter counter;
    private float attackSpeed;
    [SerializeField] protected Charger charger;
    protected ProjectileBlaster blaster = new ProjectileBlaster();
    protected Dictionary<Vector3Int, TileData> currentTileMap;
    protected MovementMode movementMode;
    protected TileData currentTile;

    public UnityEvent<Character> OnEnterdWater;
    public UnityEvent<Character> OnExitWater;
    public UnityEvent<Character> OnEnteredGround;
    public UnityEvent<Character> OnExitGround;
    public UnityEvent<Character> OnEnteredAir;
    public UnityEvent<Character> OnExitAir;

    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Effectable Effectable { get => effectable; }
    public virtual LookDirections LookingTowards { get => lookingTowards; }
    public virtual AttackCounter Counter { get => counter; }
    public virtual float AttackSpeed { get => attackSpeed; }
    public virtual TileData CurrentTile { get => currentTile; }
    public Charger Charger { get => charger; }
    public Dictionary<Vector3Int, TileData> CurrentTileMap { get => currentTileMap; }
    public MovementMode MovementMode { get => movementMode; }
    public CharacterLevel Level { get => level; }

    public virtual void AddAttackSpeed(float amount)
    {

    }

    public virtual void Stun()
    {

    }
    public virtual void EndStun()
    {

    }
    public virtual void FireProjectile(ProjectileAttack attack)
    {

    }

    public virtual void Charge(AnimalAttack attack)
    {
    }
    public virtual void UpdateCurrentTile(TileData current)
    {

    }

    public virtual void SetFlightMode(bool spawn = false)
    {
        if (spawn || CheckPlaneAvailable(GameManager.Instance.LevelManager.CurrentLevel.FlyingMap))
        {
            ExitMapEvent();
            currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.FlyingMap;
            movementMode = MovementMode.Air;
            if (!spawn)
            {
                OnEnteredAir?.Invoke(this);
            }
        }
    }
    public virtual void SetSwimMode(bool spawn = false)
    {
        if (spawn || CheckPlaneAvailable(GameManager.Instance.LevelManager.CurrentLevel.SwimmingMap))
        {
            ExitMapEvent();
            currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.SwimmingMap;
            movementMode = MovementMode.Water;
            if (!spawn)
            {
                OnEnterdWater?.Invoke(this);
            }
        }
    }
    public virtual void SetWalkMode(bool spawn = false)
    {
        if (spawn || CheckPlaneAvailable(GameManager.Instance.LevelManager.CurrentLevel.GroundMap))
        {
            ExitMapEvent();
            currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.GroundMap;
            movementMode = MovementMode.Ground;
            if (!spawn)
            {
                OnEnteredGround?.Invoke(this);
            }
        }
    }

    public virtual void SetStartTraversal()
    {

    }

    protected virtual void SetMovementModeEnum(MovementMode mode)
    {
        switch (mode)
        {
            case MovementMode.Ground:
                SetWalkMode();
                break;
            case MovementMode.Water:
                SetSwimMode();
                break;
            case MovementMode.Air:
                SetFlightMode();
                break;
            default:
                break;
        }
    }

    protected virtual bool CheckPlaneAvailable(Dictionary<Vector3Int, TileData> map)
    {
        List<TileData> neighbours = GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(CurrentTile, map);
        if (neighbours.Count > 0)
        {
            return true;
        }
        return false;
    }

    protected virtual void ExitMapEvent()
    {
        switch (movementMode)
        {
            case MovementMode.Ground:
                OnExitGround?.Invoke(this);
                break;
            case MovementMode.Water:
                OnExitWater?.Invoke(this);
                break;
            case MovementMode.Air:
                OnExitAir?.Invoke(this);
                break;
            default:
                break;
        }
    }
}


public enum MovementMode
{
    Ground,
    Water,
    Air
}