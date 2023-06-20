using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChanceBuff : StatusEffect
{
    private float amount;
    public HitChanceBuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyHitChanceBuff());
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
    private IEnumerator ApplyHitChanceBuff()
    {
        float amountToAdd = (host.DamageDealer.HitChance * amount);
        host.DamageDealer.AddHitChance(amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.DamageDealer.AddHitChance(-amountToAdd);
        host.Effectable.RemoveStatus(this);
    }
}
