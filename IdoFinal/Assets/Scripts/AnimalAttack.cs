using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AnimalAttack : ScriptableObject
{
    [SerializeField] private DamageHandler damage = new DamageHandler();
    [SerializeField] private List<StatusEffectActivationData> statusEffects = new List<StatusEffectActivationData>();
    [SerializeField] private List<Vector3Int> hitbox = new List<Vector3Int>();
    [SerializeField] private float coolDown;
    public DamageHandler Damage { get => damage; }
    public List<StatusEffectActivationData> StatusEffects { get => statusEffects; }
    public float CoolDown { get => coolDown; }

    public List<Vector3Int> Hitbox { get => hitbox; }

    private void OnEnable()
    {
        damage.ClearMods();
    }
}

[System.Serializable]
public class StatusEffectActivationData
{
    public StatusEffectEnum Effect;
    [Range(0, 100)] public float Chance;
}





