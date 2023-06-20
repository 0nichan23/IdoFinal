using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeChanceDebuff : StatusEffect
{
    private float amount;
    public DodgeChanceDebuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.NEG;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyDodgeBuff());
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

    private IEnumerator ApplyDodgeBuff()
    {
        float amountToAdd = host.Damageable.DodgeChance * amount;
        host.Damageable.AddDodgeChance(-amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.Damageable.AddDodgeChance(amountToAdd);
        host.Effectable.RemoveStatus(this);
    }
}
