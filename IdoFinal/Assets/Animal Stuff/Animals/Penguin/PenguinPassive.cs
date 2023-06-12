using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PenguinPassive", menuName = "Passives/Penguin")]
public class PenguinPassive : AnimalPassive
{
    //gain an attack speed buff after attacking anything that lives in the water
    [SerializeField, Range(5,10)] private float duration;
    [SerializeField, Range(0,1)] private float amount;

    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(AttackSpeedBuffAfterAttackingWaterCreatures);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(AttackSpeedBuffAfterAttackingWaterCreatures);
    }

    private void AttackSpeedBuffAfterAttackingWaterCreatures(Damageable target, AnimalAttack attack, DamageDealer dealer)
    {
        if (target.RefAnimal.Habitat == Habitat.FreshWater || target.RefAnimal.Habitat == Habitat.SaltWater)
        {
            dealer.RefCharacter.Effectable.AddStatus(new AttackSpeedBuff(duration, amount), dealer);
        }
    }

}
