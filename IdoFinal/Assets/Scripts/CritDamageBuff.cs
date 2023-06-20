using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritDamageBuff : StatusEffect
{
    private float amount;
    public CritDamageBuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyCritDamageBuff());
    }
    public override void Reset()
    {
        counter = 0f;
    }

    public override void Remove()
    {
        base.Remove();
        counter = duration;
    }
    private IEnumerator ApplyCritDamageBuff()
    {
        float amountToAdd = (host.DamageDealer.CritDamage * amount);
        host.DamageDealer.AddCritDamage(amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.DamageDealer.AddCritDamage(-amountToAdd);
        host.Effectable.RemoveStatus(this);
    }
}
