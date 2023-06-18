using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CheetahPassive", menuName = "Passives/Cheetah")]
public class CheetahPassive : AnimalPassive
{
    //gain a bonus (up to a max) to attack power based on speed every 3 attacks
    [SerializeField, Range(0,1)] private float maxDamageBoost;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(DamageBoost);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(DamageBoost);
    }

    private void DamageBoost(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefCharacter.Counter.CurrentCounter %3 == 0)
        {
            float boost = 0f;
            for (int i = 0; i < dealer.RefAnimal.StatSheet.Speed; i++)
            {
                boost += (maxDamageBoost / 10);
            }
            dmg.AddMod(1 + boost);
        }
    }
}
