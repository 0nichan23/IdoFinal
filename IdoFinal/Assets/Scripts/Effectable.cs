using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effectable : MonoBehaviour
{
    public UnityEvent<StatusEffect> OnObtainEffect;

    public UnityEvent<StatusEffect> OnRemoveEffect;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();
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
        OnObtainEffect?.Invoke(givenEffect);
    }
    public void RemoveStatus(StatusEffect givenEffect)
    {
        foreach (var item in activeEffects)
        {
            if (item.GetType() == givenEffect.GetType())
            {
                item.Remove();
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

    public StatusEffect GetEffectFromEnum(StatusEffectsEnum givenStatus)
    {
        switch (givenStatus)
        {
            case StatusEffectsEnum.Bleed:
                return new Poison();
            case StatusEffectsEnum.Poison:
                return new Poison();

            case StatusEffectsEnum.Stun:
                return new Poison();

            default:
                return new Poison();
        }
    }


}


public enum StatusEffectsEnum
{
    Bleed,
    Poison,
    Stun,
}