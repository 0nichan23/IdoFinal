using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public UnityEvent<Damageable, AnimalAttack> OnHit;

    public UnityEvent<AnimalAttack> OnDealDamage;

    public UnityEvent<AnimalAttack> OnDealCritDamage;

    public UnityEvent<AnimalAttack> OnDealDamageFinal;

    public UnityEvent<StatusEffect> OnApplyStatus;

    public UnityEvent<Damageable> OnKill;

    private Animal refAnimal;

    private float critChance;//base chance to crit
    private float critDamage;//base modifier added to cirtical hits
    private float powerDamageMod;

    public void SetStats(Animal givenAnimal)
    {
        OnDealDamage.RemoveListener(PowerDamageBoost);
        OnDealCritDamage.RemoveListener(CriticalDamageBoost);
        refAnimal = givenAnimal;
        powerDamageMod = GetDamageMod(refAnimal.StatSheet.Power);
        critChance = GetCritCahcne(refAnimal.StatSheet.Instinct); //base crit chance is 0 - 50%
        critDamage = GetCritDamage(refAnimal.StatSheet.Instinct);// base crit damage is 
        OnDealDamage.AddListener(PowerDamageBoost);
        OnDealCritDamage.AddListener(CriticalDamageBoost);
    }

    private float GetDamageMod(int power)
    {
        float baseDamage = 1;
        for (int i = 0; i <= power; i++)
        {
            baseDamage += 0.05f;
        }
        return baseDamage;
    }

    private float GetCritDamage(int instinct)
    {
        float baseDamage = 1;
        for (int i = 0; i <= instinct; i++)
        {
            baseDamage += 0.1f;
        }
        return baseDamage;
    }
    private float GetCritCahcne(int instinct)
    {
        float baseDamage = 0f;
        for (int i = 0; i <= instinct; i++)
        {
            baseDamage += 0.05f;
        }
        return baseDamage;
    }

    private void PowerDamageBoost(AnimalAttack givenAttack)
    {
        givenAttack.Damage.AddMod(powerDamageMod);
    }
    private void CriticalDamageBoost(AnimalAttack givenAttack)
    {
        givenAttack.Damage.AddMod(critDamage);
    }


    public float CritChance { get => critChance; }
    public float CritDamage { get => critDamage; }
    public Animal RefAnimal { get => refAnimal; }
}
