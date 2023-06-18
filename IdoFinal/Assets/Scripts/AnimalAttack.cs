using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AnimalAttack : ScriptableObject
{
    [SerializeField] private int baseDamage;
    [SerializeField] private List<Vector3Int> hitbox = new List<Vector3Int>();
    [SerializeField] private float coolDown;
    [SerializeField] private bool charge;
    [SerializeField] private Sprite artwork;
    public int Damage { get => baseDamage; }
    public float CoolDown { get => coolDown; }

    public List<Vector3Int> Hitbox { get => hitbox; }
    public bool Charge { get => charge; }
    public Sprite Artwork { get => artwork;}

}





