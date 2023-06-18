using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PronghornPassive", menuName = "Passives/Pronghorn")]
public class PronghornPassive : AnimalPassive
{
    //gain an attack speed buff after being struck by plains or desert creatures 
    [SerializeField, Range(0,10)] private float duration;
    [SerializeField, Range(0,1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(ApplyBuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(ApplyBuff);
    }

    private void ApplyBuff(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefAnimal.Habitat == Habitat.Desert || dealer.RefAnimal.Habitat == Habitat.Plains)
        {
            target.RefCharacter.Effectable.AddStatus(new AttackSpeedBuff(duration, amount), target.RefCharacter.DamageDealer);
        }
    }
}
