using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AnimalAttack : ScriptableObject
{
    [SerializeField] private DamageHandler damage = new DamageHandler();
    [SerializeField] private List<StatusEffectActivationData> statusEffects = new List<StatusEffectActivationData>();
    public DamageHandler Damage { get => damage; }
    public List<StatusEffectActivationData> StatusEffects { get => statusEffects; }
}

[System.Serializable]
public class StatusEffectActivationData
{
    public StatusEffectsEnum Effect;
    [Range(0,100)] public float Chance;
}
