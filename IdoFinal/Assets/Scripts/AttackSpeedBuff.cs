using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBuff : StatusEffect
{
    private float duration;
    private float amount;
    private float counter;
    public AttackSpeedBuff(float givenDuration, float givenAmount)
    {
        duration = givenDuration;
        amount = givenAmount;
        effectOri = EffectOrientation.POS;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(ApplyAttackSpeedBuff());
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
    private IEnumerator ApplyAttackSpeedBuff()
    {
        float amountToAdd = host.AttackSpeed * amount;
        host.AddAttackSpeed(amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.AddAttackSpeed(-amountToAdd);
        host.Effectable.RemoveStatus(this);
    }

}
