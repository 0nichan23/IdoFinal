using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritHitBuff : StatusEffect
{
    private float amount;
    public CritHitBuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyCritHitBuff());
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
    private IEnumerator ApplyCritHitBuff()
    {
        float amountToAdd = host.DamageDealer.CritChance * amount;
        host.DamageDealer.AddCritChance(amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.DamageDealer.AddCritChance(-amountToAdd);
        host.Effectable.RemoveStatus(this);
    }

}
