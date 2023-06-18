using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffect
{
    private float mod;
    private float amount = 1;
    public Burn(float duration, float mod)
    {
        this.duration = duration;
        effectOri = EffectOrientation.NEG;
        this.mod = mod;
    }
    protected override void Subscribe()
    {
        host.DamageDealer.OnHit.AddListener(DecreaseDamageDealt);
        host.StartCoroutine(BurnEffect());
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
        host.DamageDealer.OnHit.RemoveListener(DecreaseDamageDealt);
    }
    private IEnumerator BurnEffect()
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

    private void DecreaseDamageDealt(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(1 - mod);
    }



}
