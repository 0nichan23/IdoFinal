using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private Effectable effectable;
    private LookDirections lookingTowards;

    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public Effectable Effectable { get => effectable; }
    public virtual LookDirections LookingTowards { get => lookingTowards; }
}
