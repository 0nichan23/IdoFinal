using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElephantPassive", menuName = "Passives/Elephant")]

public class ElephantPassive : AnimalPassive
{
    //add armor pen to attacks
    [SerializeField, Range(1,10)] private int armorPenBoost;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.AddArmorPenetration(armorPenBoost);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.AddArmorPenetration(-armorPenBoost);
    }
}
