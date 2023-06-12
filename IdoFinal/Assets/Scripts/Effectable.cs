using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effectable : MonoBehaviour
{
    public UnityEvent<StatusEffect, Effectable> OnObtainEffect;

    public UnityEvent<StatusEffect, Effectable> OnRemoveEffect;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    private Character owner;

    public List<StatusEffect> ActiveEffects { get => activeEffects; }

    public void CahceOwner(Character givenCharacter)
    {
        owner = givenCharacter;
    }
    public void AddStatus(StatusEffect givenEffect, DamageDealer dealer)
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
        OnObtainEffect?.Invoke(givenEffect, this);
        dealer.OnApplyStatus?.Invoke(givenEffect, this, dealer);
    }
    public void RemoveStatus(StatusEffect givenEffect)
    {
        foreach (var item in activeEffects)
        {
            if (item.GetType() == givenEffect.GetType())
            {
                item.Remove();
                activeEffects.Remove(item);
                OnRemoveEffect?.Invoke(givenEffect, this);
                return;
            }
        }
    }

}



