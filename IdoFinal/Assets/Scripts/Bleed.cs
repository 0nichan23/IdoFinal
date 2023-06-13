using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffect
{
    private float amount = 1f;
    public Bleed(float duration)
    {
        this.duration = duration;
        effectOri = EffectOrientation.NEG;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(BleedDamage());
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
    private IEnumerator BleedDamage()
    {
        counter = 0f;
        while (counter < duration)
        {
            yield return new WaitForSeconds(1);
            host.Damageable.TakeTrueDamage(amount);
            counter += 1;
        }
        host.Effectable.RemoveStatus(this);
    }

}
