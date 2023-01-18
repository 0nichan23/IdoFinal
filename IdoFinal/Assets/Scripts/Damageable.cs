using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Effectable effectable;

    private float currentHp;
    private float maxHp;
    private Animal refAnimal;

    public UnityEvent<AnimalAttack> OnTakeDamage;
    public UnityEvent<AnimalAttack> OnTakeCriticalDamage;
    public UnityEvent<AnimalAttack> OnTakeDamageFinal;
    public UnityEvent OnDeath;
    public UnityEvent OnTakeDamageGFX;

    public UnityEvent<DamageHandler> OnHeal;

    public Animal RefAnimal { get => refAnimal; }
    public float MaxHp { get => maxHp; }
    public float CurrentHp { get => currentHp; }

    public void SetStats(Animal givenAnimal)
    {
        maxHp = givenAnimal.StatSheet.MaxHp;
        currentHp = maxHp;
        refAnimal = givenAnimal;
    }

    public void CacheEffectable(Effectable givenEffectable)
    {
        effectable = givenEffectable;
    }

    public void GetHit(AnimalAttack attack, DamageDealer dealer)
    {
        if (!ReferenceEquals(effectable, null))
        {
            effectable.UpdateStatuses(attack, dealer);
        }
        dealer.OnHit?.Invoke(this, attack);
        if (CheckForCritHit(dealer.CritChance))
        {
            TakeDamage(attack, dealer, true);
        }
        else
        {
            TakeDamage(attack, dealer);
        }
    }

    public void TakeDamage(AnimalAttack attack, DamageDealer dealer, bool critHit = false)
    {
        OnTakeDamage?.Invoke(attack);
        dealer.OnDealDamage?.Invoke(attack);
        if (critHit)
        {
            OnTakeCriticalDamage?.Invoke(attack);
            dealer.OnDealCritDamage?.Invoke(attack);
            Debug.Log("critical hit");
        }
        OnTakeDamageFinal?.Invoke(attack);
        dealer.OnDealDamageFinal?.Invoke(attack);
        Debug.Log(attack.Damage.CalcFinalDamageMult());
        currentHp -= attack.Damage.CalcFinalDamageMult();
        OnTakeDamageGFX?.Invoke();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            dealer.OnKill?.Invoke(this);
        }
        attack.Damage.ClearMods();
        ClampHp();

    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    public void Heal(DamageHandler givenDamage)
    {
        OnHeal?.Invoke(givenDamage);
        currentHp += givenDamage.CalcFinalDamageMult();
        ClampHp();
    }

    private bool CheckForCritHit(float chance)
    {
        float c = chance * 100;
        if (Random.Range(0,100) < c)
        {
            return true;
        }
        return false;
    }

}
