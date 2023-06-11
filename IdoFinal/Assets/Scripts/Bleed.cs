using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffect
{
    private float counter;
    private float duration;
    public Bleed(float duration)
    {
        this.duration = duration;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(BleedDamage());
    }

    public override void Reset()
    {
        counter = 0f;
    }

    private IEnumerator BleedDamage()
    {
        counter = 0f;
        while (counter < duration)
        {
            host.Damageable.TakeTrueDamage(1f);
            yield return new WaitForSeconds(1);
            counter += 1;
        }
    }

}
