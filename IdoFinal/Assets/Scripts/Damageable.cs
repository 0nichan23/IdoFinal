using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Effectable effectable;

    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    private float extraMaxHp;
    private Animal refAnimal;
    private Character refCharacter;

    public UnityEvent<AnimalAttack, Damageable, DamageDealer, DamageHandler> OnGetHit;
    public UnityEvent<AnimalAttack, Damageable, DamageDealer, DamageHandler> OnTakeCriticalDamage;
    public UnityEvent<AnimalAttack, Damageable, DamageDealer, DamageHandler> OnTakeDamageFinal;
    public UnityEvent OnDeath;
    public UnityEvent OnTakeDamageGFX;
    public UnityEvent<DamageHandler> OnHeal;
    public UnityEvent OnHealGFX;

    private float basedamageReduction;
    private float damageReduction;

    private float basedodgeChance;
    private float dodgeChance;

    public bool EmitPopups;
    public Animal RefAnimal { get => refAnimal; }
    public float MaxHp { get => maxHp + extraMaxHp; }
    public float CurrentHp { get => currentHp; }
    public float DamageReduction { get => Mathf.Clamp(basedamageReduction + damageReduction, 0.1f, 1f); }
    public float DodgeChance { get => Mathf.Clamp(basedodgeChance + dodgeChance, 0f, 0.9f); }
    public Character RefCharacter { get => refCharacter; }

    public void SetStats(Animal givenAnimal, Character givenCharacter)
    {
        refCharacter = givenCharacter;
        OnGetHit.RemoveListener(DamageReductionBoost);
        maxHp = givenAnimal.StatSheet.MaxHp;
        currentHp = MaxHp;
        refAnimal = givenAnimal;
        basedamageReduction = GetBaseDamageReduction(RefAnimal.StatSheet.Defense);
        basedodgeChance = GetBaseDodgeChance(RefAnimal.StatSheet.Speed);
        OnGetHit.AddListener(DamageReductionBoost);
    }

    public void IncreaseMaxHp(int amount)
    {
        extraMaxHp += amount;
        currentHp = MaxHp;
    }

    private float GetBaseDamageReduction(int toughness)
    {
        float baseAmount = 0f;
        for (int i = 0; i < toughness; i++)
        {
            baseAmount += 0.05f;//a total of 50% damage reduction if the score is 10
        }
        return 1 - baseAmount;
    }

    private float GetBaseDodgeChance(int speed)
    {
        float baseAmount = 0f;
        for (int i = 0; i < speed; i++)
        {
            baseAmount += 0.03f;//a total of 30% chance to dodge if speed is at 10
        }
        return baseAmount;
    }

    public void AddDodgeChance(float amount)
    {
        dodgeChance += amount;
    }
    public void AddDamageReduction(float amount)
    {
        damageReduction -= amount;
    }
    private void DamageReductionBoost(AnimalAttack givenAttack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(DamageReduction);
    }

    public void CacheEffectable(Effectable givenEffectable)
    {
        effectable = givenEffectable;
    }

    public void GetHit(AnimalAttack attack, DamageDealer dealer)
    {
        if (!CheckForHit(dealer.HitChance))
        {
            if (EmitPopups)
            {
                GameManager.Instance.PopupSpawner.SpawnMissPopup(transform.position);
            }
            return;
        }
        DamageHandler dmg = new DamageHandler() { BaseAmount = attack.Damage };
        OnGetHit?.Invoke(attack, this, dealer, dmg);
        dealer.OnHit?.Invoke(this, attack, dealer, dmg);
        if (CheckForCritHit(dealer.CritChance))
        {
            TakeDamage(attack, dealer, dmg, true);
        }
        else
        {
            TakeDamage(attack, dealer, dmg);
        }
    }

    public void TakeDamage(AnimalAttack attack, DamageDealer dealer, DamageHandler dmg, bool critHit = false)
    {
        if (critHit)
        {
            OnTakeCriticalDamage?.Invoke(attack, this, dealer, dmg);
            dealer.OnDealCritDamage?.Invoke(attack, this, dealer, dmg);
        }
        OnTakeDamageFinal?.Invoke(attack, this, dealer, dmg);
        dealer.OnDealDamageFinal?.Invoke(attack, this, dealer, dmg);
        if (EmitPopups)
        {
            if (critHit)
            {
                GameManager.Instance.PopupSpawner.SpawnCritDamagePopup(transform.position, dmg.CalcFinalDamageMult());
            }
            else
            {
                GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, dmg.CalcFinalDamageMult());
            }
        }
        currentHp -= Mathf.RoundToInt(dmg.CalcFinalDamageMult());
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            dealer.OnKill?.Invoke(this, dealer);
        }
        dmg.ClearMods();
        ClampHp();
        OnTakeDamageGFX?.Invoke();

    }

    public void TakeTrueDamage(float fixedAmount, DamageDealer dealer = null)
    {
        currentHp -= Mathf.RoundToInt(fixedAmount);
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, Mathf.RoundToInt(fixedAmount), Color.yellow);
        }
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            if (!ReferenceEquals(dealer, null))
            {
                dealer.OnKill?.Invoke(this, dealer);
            }
        }
        ClampHp();
        OnTakeDamageGFX?.Invoke();
    }

    /*multihits occur only after an attack hit in the first place, they can miss and deal 
    flat damage equal to the damage of the attack that triggered the effect.
    multihits CANNOT trigger any events at all*/
    public void GetMultiHit(DamageDealer dealer, float totalDamage)
    {
        if (!CheckForHit(dealer.HitChance))
        {
            if (EmitPopups)
            {
                GameManager.Instance.PopupSpawner.SpawnMissPopup(transform.position);
            }
            return;
        }
        TakeTrueDamage(totalDamage, dealer);
    }

    public void HealTrueDamage(float fixedAmount)
    {
        currentHp += fixedAmount;
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, fixedAmount, Color.green);
        }
        ClampHp();
        OnHealGFX?.Invoke();
    }
    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
    }

    public void Heal(DamageHandler givenDamage)
    {
        OnHeal?.Invoke(givenDamage);
        currentHp += givenDamage.CalcFinalDamageMult();
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, givenDamage.CalcFinalDamageMult(), Color.green);
        }
        ClampHp();
        OnHealGFX?.Invoke();
    }

    private bool CheckForCritHit(float chance)
    {
        float c = chance * 100;
        if (Random.Range(0, 100) <= c)
        {
            return true;
        }
        return false;
    }

    private bool CheckForHit(float chance)
    {
        float c = chance * 100;
        float d = DodgeChance * 100;
        c -= d;
        c = Mathf.Clamp(c, 1, 100); //there is always at least 1% chance to hit the target
        if (Random.Range(0, 100) <= c)
        {
            return true;
        }
        return false;
    }
}

