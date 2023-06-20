using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritDamageDebuff : StatusEffect
{
    private float amount;
    public CritDamageDebuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.NEG;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyCritDamageDeBuff());
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
    private IEnumerator ApplyCritDamageDeBuff()
    {
        float amountToAdd = (host.DamageDealer.CritDamage * amount);
        host.DamageDealer.AddCritDamage(-amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.DamageDealer.AddCritDamage(amountToAdd);
        host.Effectable.RemoveStatus(this);
    }
}
