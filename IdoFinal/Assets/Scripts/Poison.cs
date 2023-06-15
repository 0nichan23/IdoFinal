using System.Collections;
using UnityEngine;

public class Poison : StatusEffect
{
    private float amount;
    private float totalAmount;
    public Poison(float duration, float totalAmount)
    {
        this.duration = duration;
        this.totalAmount = totalAmount;
        effectOri = EffectOrientation.NEG;
    }
    protected override void Subscribe()
    {
        host.StartCoroutine(PoisonDmg());
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
    private IEnumerator PoisonDmg()
    {
        counter = 0f;
        amount = totalAmount / duration;
        while (counter < duration)
        {
            yield return new WaitForSeconds(1);
            counter += 1;
            host.Damageable.TakeTrueDamage(amount);
        }
        host.Effectable.RemoveStatus(this);
    }

}
