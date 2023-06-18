using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TigerPassive", menuName = "Passives/Tiger")]
public class TigerPassive : AnimalPassive
{
    //the bigger they are the more crit damage they take
    [SerializeField, Range(0, 1)] private float smallBuff;
    [SerializeField, Range(0, 1)] private float mediumBuff;
    [SerializeField, Range(0, 1)] private float largeBuff;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(IncreaseCritDamage);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.RemoveListener(IncreaseCritDamage);
    }

    private void IncreaseCritDamage(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        switch (target.RefAnimal.Size)
        {
            case Size.Small:
                dmg.AddMod(smallBuff);
                break;
            case Size.Medium:
                dmg.AddMod(mediumBuff);
                break;
            case Size.Large:
                dmg.AddMod(largeBuff);
                break;
        }
    }
}
