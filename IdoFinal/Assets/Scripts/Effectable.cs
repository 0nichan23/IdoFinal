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
    public void AddStatus(StatusEffect givenEffect, DamageDealer dealer = null)
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
        dealer.OnApplyStatus?.Invoke(givenEffect);
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

}



