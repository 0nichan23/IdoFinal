using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClownFishPassive", menuName = "Passives/ClownFish")]
public class ClownFishPassive : AnimalPassive
{
    //gain true heal when dealing damage in salt water
    [SerializeField, Range(0, 10)] private int amount;
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
        if (GameManager.Instance.LevelManager.CurrentLevel.Habitat ==  Habitat.SaltWater)
        {
            dealer.RefCharacter.Damageable.HealTrueDamage(amount);
        }
    }
}
