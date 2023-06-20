using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuff : StatusEffect
{
    private float amount;
    public DefenseBuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyDefenseBuff());
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
    private IEnumerator ApplyDefenseBuff()
    {
        float amountToAdd = host.Damageable.DamageReduction * amount;
        host.Damageable.AddDamageReduction(amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.Damageable.AddDamageReduction(-amountToAdd);
        host.Effectable.RemoveStatus(this);
    }

}
