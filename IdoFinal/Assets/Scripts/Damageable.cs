using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Effectable effectable;

    private float currentHp;
    private float maxHp;

    public UnityEvent<AnimalAttack> OnTakeDamage;
    public UnityEvent<AnimalAttack> OnTakeDamageFinal;
    public UnityEvent OnDeath;

    public UnityEvent OnTakeDamageGFX;

    private Animal refAnimal;

    public Animal RefAnimal { get => refAnimal; }

    public void SetStats(Animal givenAnimal)
    {
        this.maxHp = givenAnimal.StatSheet.MaxHp;
        this.effectable = givenAnimal.Effectable;
        refAnimal = givenAnimal;
    }

    public void GetHit(AnimalAttack attack, DamageDealer dealer)
    {
        if (!ReferenceEquals(effectable, null))
        {
            effectable.UpdateStatuses(attack, dealer);
        }
        dealer.OnHit?.Invoke(this, attack);
        TakeDamage(attack, dealer);
    }

    public void TakeDamage(AnimalAttack attack, DamageDealer dealer)
    {
        OnTakeDamage?.Invoke(attack);
        dealer.OnDealDamage?.Invoke(attack);
        OnTakeDamageFinal?.Invoke(attack);
        dealer.OnDealDamageFinal?.Invoke(attack);
        currentHp -= attack.Damage.CalcFinalDamage();
        OnTakeDamageGFX?.Invoke();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            dealer.OnKill?.Invoke();
        }
        attack.Damage.ClearMods();
        ClampHp();

    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }
}
