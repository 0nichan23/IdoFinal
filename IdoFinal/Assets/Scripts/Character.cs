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
    protected ProjectileBlaster blaster = new ProjectileBlaster();

    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Effectable Effectable { get => effectable; }
    public virtual LookDirections LookingTowards { get => lookingTowards; }
    public virtual AttackCounter Counter { get => counter; }
    public virtual float AttackSpeed { get => attackSpeed;}

    public virtual void AddAttackSpeed(float amount)
    {

    }

    public virtual void Stun()
    {

    }
    public virtual void EndStun()
    {

    }
    public virtual void FireProjectile(AnimalAttack attack)
    {

    }
}
