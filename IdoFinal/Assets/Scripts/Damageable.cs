using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Effectable effectable;

    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    private Animal refAnimal;
    private Character refCharacter;

    public UnityEvent<AnimalAttack, Damageable, DamageDealer> OnGetHit;
    public UnityEvent<AnimalAttack> OnTakeDamage;
    public UnityEvent<AnimalAttack> OnTakeCriticalDamage;
    public UnityEvent<AnimalAttack> OnTakeDamageFinal;
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
    public float MaxHp { get => maxHp; }
    public float CurrentHp { get => currentHp; }
    public float DamageReduction { get => Mathf.Clamp(basedamageReduction + damageReduction, 0.1f, 1f); }
    public float DodgeChance { get => Mathf.Clamp(basedodgeChance + dodgeChance, 0f, 0.9f); }
    public Character RefCharacter { get => refCharacter; }

    public void SetStats(Animal givenAnimal, Character givenCharacter)
    {
        refCharacter = givenCharacter;
        OnTakeDamage.RemoveListener(DamageReductionBoost);
        maxHp = givenAnimal.StatSheet.MaxHp;
        currentHp = maxHp;
        refAnimal = givenAnimal;
        basedamageReduction = GetBaseDamageReduction(RefAnimal.StatSheet.Defense);
        basedodgeChance = GetBaseDodgeChance(RefAnimal.StatSheet.Speed);
        OnTakeDamage.AddListener(DamageReductionBoost);
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
    private void DamageReductionBoost(AnimalAttack givenAttack)
    {
        givenAttack.Damage.AddMod(DamageReduction);
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
        OnGetHit?.Invoke(attack, this, dealer);
        dealer.OnHit?.Invoke(this, attack, dealer);
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
            dealer.OnDealCritDamage?.Invoke(attack, this, dealer);
        }
        OnTakeDamageFinal?.Invoke(attack);
        dealer.OnDealDamageFinal?.Invoke(attack, this, dealer);
        if (EmitPopups)
        {
            if (critHit)
            {
                GameManager.Instance.PopupSpawner.SpawnCritDamagePopup(transform.position, attack.Damage.CalcFinalDamageMult());
            }
            else
            {
                GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, attack.Damage.CalcFinalDamageMult());
            }
        }
        currentHp -= Mathf.RoundToInt(attack.Damage.CalcFinalDamageMult());
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            dealer.OnKill?.Invoke(this, dealer);
        }
        attack.Damage.ClearMods();
        ClampHp();
        OnTakeDamageGFX?.Invoke();

    }

    public void TakeTrueDamage(float fixedAmount)
    {
        currentHp -= Mathf.RoundToInt(fixedAmount);
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, Mathf.RoundToInt(fixedAmount), Color.yellow);
        }
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
        ClampHp();
        OnTakeDamageGFX?.Invoke();
    }

    public void HealTrueDamage(float fixedAmount)
    {
        currentHp += fixedAmount;
        if (EmitPopups)
        {
            GameManager.Instance.PopupSpawner.SpawnDamagePopup(transform.position, fixedAmount, Color.green);
        }
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
        ClampHp();
        OnHealGFX?.Invoke();
    }
    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
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

