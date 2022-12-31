using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Effectable effectable;

    private float currentHp;
    private float maxHp;

    public UnityEvent<AnimalAttack> OnTakeDamage;
    public UnityEvent<AnimalAttack> OnTakeDamageFinal;
    public UnityEvent OnDeath;

    public UnityEvent OnTakeDamageGFX;
    
    public void SetStats(float maxHp)
    {
        this.maxHp = maxHp;
    }

    public void GetHit(AnimalAttack attack, DamageDealer dealer)
    {
        if (!ReferenceEquals(effectable, null))
        {
            effectable.UpdateStatuses(attack, dealer);
        }
        TakeDamage(attack, dealer);
    }

    public void TakeDamage(AnimalAttack attack, DamageDealer dealer)
    {
        OnTakeDamage?.Invoke(attack);
        dealer.OnDealDamage?.Invoke(attack);
        OnTakeDamageGFX?.Invoke();
        currentHp -= attack.Damage.CalcFinalDamage();
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
