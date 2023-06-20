using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDebuff : StatusEffect
{
    private float amount;

    public DamageDebuff(float duration, float amount)
    {
        this.duration = duration;
        this.amount = amount;
        effectOri = EffectOrientation.NEG;
    }

    public override void Reset()
    {
        counter = 0f;
    }

    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyDamageBuff());
    }

    public override void Remove()
    {
        base.Remove();
        counter = duration;
    }
    private IEnumerator ApplyDamageBuff()
    {
        float amountToAdd = host.DamageDealer.PowerDamageMod * amount;
        host.DamageDealer.AddAttackDamage(-amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.DamageDealer.AddAttackDamage(amountToAdd);
        host.Effectable.RemoveStatus(this);
    }

}
