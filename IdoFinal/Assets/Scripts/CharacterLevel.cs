using UnityEngine;
using UnityEngine.Events;

public class CharacterLevel : MonoBehaviour
{
    //levels are going to range from 1 to 100
    [SerializeField, Range(1, 100)] private int level = 1;
    [SerializeField, Range(10, 20)] private int levelMod;
    [SerializeField, Range(0, 0.05f)] private float powerMod;
    private float totalXp;
    private float xpToNextLevel;
    private Character refCharacter;
    public UnityEvent<Character> OnLevelUp;

    public int Level { get => level; }
    public float XpToNextLevel { get => xpToNextLevel; }
    public float TotalXp { get => totalXp; }

    public void SetUp(Character givenCharacter, int startingLevel = 1)
    {
        refCharacter = givenCharacter;
        refCharacter.Damageable.IncreaseMaxHp(5 * (startingLevel - 1));
        refCharacter.DamageDealer.OnHit.RemoveListener(LevelDamageIncrease);
        level = startingLevel;
        xpToNextLevel = (level + 1) * levelMod;
        totalXp = 0;
        refCharacter.DamageDealer.OnHit.AddListener(LevelDamageIncrease);
    }

    public void LevelUp()
    {
        xpToNextLevel = (level + 1) * levelMod;
        level += 1;
        totalXp = 0;
        OnLevelUp?.Invoke(refCharacter);
    }

    public void GainXp(float amount)
    {
        totalXp += amount;
        if (totalXp >= xpToNextLevel)
        {
            LevelUp();
        }
    }
    public float GetLevelDamageBoost()
    {
        return level * powerMod;
    }

    private void LevelDamageIncrease(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(1 + GetLevelDamageBoost());
    }

}
