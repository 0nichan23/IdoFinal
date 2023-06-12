using System.Collections;
using UnityEngine;

public class DamageBuff : StatusEffect
{
    private float duration;
    private float amount;
    private float counter;

    public DamageBuff(float duration, float amount)
    {
        this.duration = duration;
        this.amount = amount;
        effectOri = EffectOrientation.POS;
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
        host.DamageDealer.AddAttackDamage(amountToAdd);
        counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        host.DamageDealer.AddAttackDamage(-amountToAdd);
        host.Effectable.RemoveStatus(this);
    }


}
