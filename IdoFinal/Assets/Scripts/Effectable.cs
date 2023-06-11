using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effectable : MonoBehaviour
{
    public UnityEvent<StatusEffect> OnObtainEffect;

    public UnityEvent<StatusEffect> OnRemoveEffect;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    private Character owner;

    public void CahceOwner(Character givenCharacter)
    {
        owner = givenCharacter;
    }
    public void AddStatus(StatusEffect givenEffect)
    {
        foreach (var item in activeEffects)
        {
            if (item.GetType() == givenEffect.GetType())
            {
                item.Reset();
                return;
            }
        }
        activeEffects.Add(givenEffect);
        givenEffect.CacheHost(owner);
        givenEffect.Activate();
        OnObtainEffect?.Invoke(givenEffect);
    }
    public void RemoveStatus(StatusEffect givenEffect)
    {
        foreach (var item in activeEffects)
        {
            if (item.GetType() == givenEffect.GetType())
            {
                item.Remove();
                activeEffects.Remove(item);
                return;
            }
        }
    }

    public void UpdateStatuses(AnimalAttack givenAttack, DamageDealer dealer)
    {
        foreach (var item in givenAttack.StatusEffects)
        {
            if (Random.Range(0, 100) >= item.Chance)
            {
                AddStatus(GetEffectFromEnum(item.Effect));
                dealer.OnApplyStatus?.Invoke(GetEffectFromEnum(item.Effect));
            }
        }
    }


    public StatusEffect GetEffectFromEnum(StatusEffectEnum givenStatus)
    {
        switch (givenStatus)
        {
            case StatusEffectEnum.Bleed:
                break;
            case StatusEffectEnum.Poison:
                break;
            case StatusEffectEnum.Blind:
                break;
            case StatusEffectEnum.Strangled:
                break;
            case StatusEffectEnum.Stunned:
                break;
            case StatusEffectEnum.DodgeBuff:
                break;
            case StatusEffectEnum.AttackSpeedBuff:
                break;
            case StatusEffectEnum.AttackDamageBuff:
                break;
            case StatusEffectEnum.CritHitBuff:
                break;
            case StatusEffectEnum.CritDmgBuff:
                break;
            case StatusEffectEnum.SpeedBuff:
                break;
            case StatusEffectEnum.DefenseBuff:
                break;
            default:
                break;
        }
        return new DodgeChanceBuff(0, 0);
    }

}

public enum StatusEffectEnum
{
    Bleed,
    Poison,
    Blind,
    Strangled,
    Stunned,
    DodgeBuff,
    AttackSpeedBuff,
    AttackDamageBuff,
    CritHitBuff,
    CritDmgBuff,
    SpeedBuff,
    DefenseBuff
}


