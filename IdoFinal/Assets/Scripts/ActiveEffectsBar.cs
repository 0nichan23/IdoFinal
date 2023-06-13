using System.Collections.Generic;
using UnityEngine;

public class ActiveEffectsBar : MonoBehaviour
{
    [SerializeField] private ActiveEffectIcon iconPrefab;

    [Header("EffectsSprites")]
    [SerializeField] private Sprite attackDamageBuff;
    [SerializeField] private Sprite attackSpeedBuff;
    [SerializeField] private Sprite damageReductionBuff;
    [SerializeField] private Sprite dodgeChanceBuff;
    [SerializeField] private Sprite critChanceBuff;
    [SerializeField] private Sprite critDamageBuff;
    [SerializeField] private Sprite hitChanceBuff;
    [SerializeField] private Sprite poison;
    [SerializeField] private Sprite bleed;
    [SerializeField] private Sprite stunned;
    [SerializeField] private Sprite regeneration;

    private List<ActiveEffectIcon> activeIcons = new List<ActiveEffectIcon>();

    public void AddEffect(StatusEffect effect)
    {
        ActiveEffectIcon newIcon = Instantiate(iconPrefab, transform);
        newIcon.SetUp(effect, GetSpriteFromStatusEffect(effect));
        activeIcons.Add(newIcon);
        newIcon.Effect.Host.Effectable.OnRemoveEffect.AddListener(RemoveIcon);
    }

    public void UpdateCounters()
    {
        if (activeIcons.Count == 0)
        {
            return;
        }
        foreach (var item in activeIcons)
        {
            item.UpdateCouner();
        }
    }
    private void RemoveIcon(StatusEffect effect, Effectable host)
    {
        Debug.Log("removing effect icon");
        foreach (var item in activeIcons)
        {
            if (item.Effect.GetType() == effect.GetType())
            {
                activeIcons.Remove(item);
                Destroy(item.gameObject);
                break;
            }
        }
    }


    public Sprite GetSpriteFromStatusEffect(StatusEffect effect)
    {
        if (effect is DamageBuff)
        {
            return attackDamageBuff;
        }
        else if (effect is AttackSpeedBuff)
        {
            return attackSpeedBuff;
        }
        else if (effect is DodgeChanceBuff)
        {
            return dodgeChanceBuff;
        }
        else if (effect is Bleed)
        {
            return bleed;
        }
        else if (effect is Regeneration)
        {
            return regeneration;
        }
        else if (effect is Stun)
        {
            return stunned;
        }
        return null;
    }

}
