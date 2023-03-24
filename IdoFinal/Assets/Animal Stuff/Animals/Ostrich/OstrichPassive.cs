using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OstrichPassive", menuName = "Passives/Ostrich")]
public class OstrichPassive : AnimalPassive
{
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.AddDodgeChance(0.2f);
        Debug.Log("added dodge chance from ostritch passive and is now at " + givenCaharacter.Damageable.DodgeChance);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.AddDodgeChance(-0.2f);
    }

}
