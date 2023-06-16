using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AnimalAttack : ScriptableObject
{
    [SerializeField] private DamageHandler damage = new DamageHandler();
    [SerializeField] private List<Vector3Int> hitbox = new List<Vector3Int>();
    [SerializeField] private float coolDown;
    [SerializeField] private bool projectile;
    public DamageHandler Damage { get => damage; }
    public float CoolDown { get => coolDown; }

    public List<Vector3Int> Hitbox { get => hitbox; }
    public bool Projectile { get => projectile; }

    private void OnEnable()
    {
        damage.ClearMods();
    }
}





