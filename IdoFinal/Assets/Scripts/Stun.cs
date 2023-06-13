using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    public Stun(float duration)
    {
        this.duration = duration;
        effectOri = EffectOrientation.NEG;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(StunTarget());
    }

    public override void Reset()
    {
        counter = 0f;
    }
    public override void Remove()
    {
        base.Remove();
        counter = duration;
        host.EndStun();
    }
    private IEnumerator StunTarget()
    {
        counter = 0f;
        host.Stun();
        while (counter < duration)
        {
            yield return new WaitForSeconds(1);
            counter += 1;
        }
        host.EndStun();
        host.Effectable.RemoveStatus(this);
    }


}
