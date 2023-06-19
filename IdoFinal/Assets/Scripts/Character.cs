using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Effectable effectable;
    public UnityEvent<Level, Character> OnEnteredLevel;
    public UnityEvent<Level, Character> OnExitLevel;
    private LookDirections lookingTowards;
    private AttackCounter counter;
    private float attackSpeed;
    [SerializeField] protected Charger charger;
    protected ProjectileBlaster blaster = new ProjectileBlaster();
    protected List<TileData> currentTileMap;
    protected MovementMode movementMode;
    protected TileData currentTile;
    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Effectable Effectable { get => effectable; }
    public virtual LookDirections LookingTowards { get => lookingTowards; }
    public virtual AttackCounter Counter { get => counter; }
    public virtual float AttackSpeed { get => attackSpeed; }
    public virtual TileData CurrentTile { get => currentTile; }
    public Charger Charger { get => charger; }
    public List<TileData> CurrentTileMap { get => currentTileMap; }
    public MovementMode MovementMode { get => movementMode; }

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

    public virtual void SetFlightMode()
    {
        if (CheckPlaneAvailable(GameManager.Instance.LevelManager.CurrentLevel.FlyingMap))
        {
            currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.FlyingMap;
            movementMode = MovementMode.Air;
        }
    }
    public virtual void SetSwimMode()
    {
        if (CheckPlaneAvailable(GameManager.Instance.LevelManager.CurrentLevel.SwimmingMap))
        {
            currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.SwimmingMap;
            movementMode = MovementMode.Water;
        }
    }
    public virtual void SetWalkMode()
    {
        if (CheckPlaneAvailable(GameManager.Instance.LevelManager.CurrentLevel.TraversableGround))
        {
            currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.TraversableGround;
            movementMode = MovementMode.Ground;
        }
    }

    protected virtual bool CheckPlaneAvailable(List<TileData> map)
    {
        List<TileData> neighbours = GameManager.Instance.LevelManager.CurrentLevel.GetNeighbours(CurrentTile, map);
        if (neighbours.Count > 0)
        {
            return true;
        }
        return false;
    }
}


public enum MovementMode
{
    Ground,
    Water,
    Air
}