using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPenBuff : StatusEffect
{

    private int amount;
    public ArmorPenBuff(float givenDuration, int givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyArmorPenBuff());
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
    private IEnumerator ApplyArmorPenBuff()
    {
        host.DamageDealer.AddArmorPenetration(amount);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.AddAttackSpeed(-amount);
        host.Effectable.RemoveStatus(this);
    }


}
