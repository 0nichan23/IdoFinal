using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CamelPassive", menuName = "Passives/Camel")]
public class CamelPassive : AnimalPassive
{
    //true heal when dealing damage in desert
    [SerializeField, Range(0, 10)] private int healAmount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(HealOnHit);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(HealOnHit);

    }

    private void HealOnHit(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (GameManager.Instance.LevelManager.CurrentLevel.Habitat == Habitat.Desert)
        {
            dealer.RefCharacter.Damageable.HealTrueDamage(healAmount);
        }
    }

}
