using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
public class ProjectileAttack : AnimalAttack
{
    [SerializeField] private Element element;
    //add range and speed later
    public Element Element { get => element;}
}

public enum Element
{
    Lightning,
    Fire,
    Poison,
    Ice
}