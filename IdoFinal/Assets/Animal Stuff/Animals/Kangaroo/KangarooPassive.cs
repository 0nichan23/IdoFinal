using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KangarooPassive", menuName = "Passives/Kangaroo")]
public class KangarooPassive : AnimalPassive
{
    //chance to stun small and medium enemies on hit
    [SerializeField, Range(0, 100)] private float smallChance;
    [SerializeField, Range(0, 100)] private float mediumChance;
    [SerializeField, Range(0, 10)] private float stunDuration;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(StunTarget);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(StunTarget);
    }

    private void StunTarget(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        switch (target.RefAnimal.Size)
        {
            case Size.Small:
                if (Random.Range(0,100) <= smallChance)
                {
                    target.RefCharacter.Effectable.AddStatus(new Stun(stunDuration), dealer);
                }          
                break;
            case Size.Medium:
                if (Random.Range(0, 100) <= mediumChance)
                {
                    target.RefCharacter.Effectable.AddStatus(new Stun(stunDuration), dealer);
                }
                break;
            case Size.Large:
                return;
        }
    }
}
