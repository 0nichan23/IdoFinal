using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public UnityEvent<Damageable, AnimalAttack, DamageDealer> OnHit;

    public UnityEvent<AnimalAttack> OnDealDamage;

    public UnityEvent<AnimalAttack, Damageable, DamageDealer> OnDealCritDamage;

    public UnityEvent<AnimalAttack> OnDealDamageFinal;

    public UnityEvent<StatusEffect, Effectable, DamageDealer> OnApplyStatus;

    public UnityEvent<Damageable> OnKill;

    private Animal refAnimal;

    private Character refCharacter;


    //base stats depend on the active animal,
    //the other stat is changed from passives, items, and team comps

    private float critChance;
    private float basecritChance;


    private float critDamage;
    private float basecritDamage;

    private float powerDamageMod;
    private float basepowerDamageMod;

    private float hitChance;
    private float basehitChance;

    private int armorPenetration;//armor pen is only directly affected from passives and items, no base stats at all

    public Animal RefAnimal { get => refAnimal; }
    public float CritChance { get => critChance + basecritChance; }
    public float CritDamage { get => critDamage + basecritDamage; }
    public float PowerDamageMod { get => powerDamageMod + basepowerDamageMod; }
    public float HitChance { get => hitChance + basehitChance; }
    public int ArmorPenetration { get => Mathf.Clamp(armorPenetration, 0, 10);}
    public Character RefCharacter { get => refCharacter;}

    public void SetStats(Animal givenActiveAnimal, Character givenCharacter)
    {
        refCharacter = givenCharacter;
        OnDealDamage.RemoveListener(PowerDamageBoost);
        OnDealCritDamage.RemoveListener(CriticalDamageBoost);
        OnHit.RemoveListener(ArmorPenBoost);
        refAnimal = givenActiveAnimal;
        basepowerDamageMod = GetDamageMod(refAnimal.StatSheet.Power);
        basecritChance = GetBaseCritCahcne(refAnimal.StatSheet.Instinct);
        basecritDamage = GetBaseCritDamage(refAnimal.StatSheet.Instinct);
        basehitChance = GetBaseHitChanceMod(refAnimal.StatSheet.Instinct);
        OnDealDamage.AddListener(PowerDamageBoost);
        OnHit.AddListener(ArmorPenBoost);
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

    private float GetBaseCritDamage(int instinct)
    {
        float baseDamage = 1;
        for (int i = 0; i < instinct; i++)
        {
            baseDamage += 0.1f;
        }
        return baseDamage;
    }
    private float GetBaseCritCahcne(int instinct)
    {
        float baseDamage = 0f;
        for (int i = 0; i <= instinct; i++)
        {
            baseDamage += 0.05f;
        }
        return baseDamage;
    }

    private float GetBaseHitChanceMod(int instinct)
    {
        float baseChance = 0.9f; //base chance to hit is always 90% 
        for (int i = 0; i <= instinct; i++)
        {
            baseChance += 0.05f;//will get up to 100% if instinct is 10
        }
        return baseChance;
    }

    private float GetArmorPen(Damageable target)
    {
        float pen = 0f;
        for (int i = 0; i < ArmorPenetration; i++)
        {
            pen += 0.05f; //can pen up to 50% armor
        }
        pen *= target.DamageReduction;
        pen += 1;
        return pen;
    }



    private void PowerDamageBoost(AnimalAttack givenAttack)
    {
        givenAttack.Damage.AddMod(PowerDamageMod);
    }
    private void CriticalDamageBoost(AnimalAttack givenAttack, Damageable target, DamageDealer dealer)
    {
        givenAttack.Damage.AddMod(CritDamage);
    }

    private void ArmorPenBoost(Damageable target, AnimalAttack givenAttack, DamageDealer dealer)
    {
        givenAttack.Damage.AddMod(GetArmorPen(target));
    }
  

    public void AddArmorPenetration(int amount)
    {
        armorPenetration += amount;
    }

    public void AddCritChance(float amount)
    {
        critChance += amount;
    }
    public void AddCritDamage(float amount)
    {
        critDamage += amount;
    }
    public void AddAttackDamage(float amount)
    {
        powerDamageMod += amount;
    }
    public void AddHitChance(float amount)
    {
        hitChance += amount;
    }
}
