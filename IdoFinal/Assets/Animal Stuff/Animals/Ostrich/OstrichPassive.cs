using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OstrichPassive", menuName = "Passives/Ostrich")]
public class OstrichPassive : AnimalPassive
{

    //add dodge chance (in desert and plains once we implement the level system)
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.AddDodgeChance(0.2f);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.AddDodgeChance(-0.2f);
    }

}
