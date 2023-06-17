using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BatPassive", menuName = "Passives/Bat")]
public class BatPassive : AnimalPassive
{
    //increased accuracy 
    [SerializeField, Range(0,1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.AddHitChance(amount);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.AddHitChance(-amount);
    }
}
