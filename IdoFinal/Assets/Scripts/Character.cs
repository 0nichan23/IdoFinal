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
    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Effectable Effectable { get => effectable; }
    public virtual LookDirections LookingTowards { get => lookingTowards; }
    public virtual AttackCounter Counter { get => counter; }
    public virtual float AttackSpeed { get => attackSpeed; }
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
        currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.FlyingMap;
        movementMode = MovementMode.Air;
    }
    public virtual void SetSwimMode()
    {
        currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.SwimmingMap;
        movementMode = MovementMode.Water;

    }
    public virtual void SetWalkMode()
    {
        currentTileMap = GameManager.Instance.LevelManager.CurrentLevel.TraversableGround;
        movementMode = MovementMode.Ground;
    }
}


public enum MovementMode
{
    Ground,
    Water,
    Air
}