using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : StatusEffect
{
    private float amount;
    public Regeneration(float duration)
    {
        this.duration = duration;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        amount = host.Damageable.MaxHp / 10;
        host.StartCoroutine(Regen());
    }

    public override void Reset()
    {
        counter = 0f;
    }
    public override void Remove()
    {
        base.Remove();
        counter = duration;
        amount = 0f;
    }
    private IEnumerator Regen()
    {
        counter = 0f;
        while (counter < duration)
        {
            yield return new WaitForSeconds(1);
            host.Damageable.HealTrueDamage(amount);
            counter += 1;
        }
        host.Effectable.RemoveStatus(this);
    }
}
