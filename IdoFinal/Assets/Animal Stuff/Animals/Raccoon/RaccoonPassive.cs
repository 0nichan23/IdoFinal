using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RaccoonPassive", menuName = "Passives/Raccoon")]
public class RaccoonPassive : AnimalPassive
{
    //deal more damage the more resources you gathered up to a maximum- system not yet implemented 
    public override void SubscribePassive(Character givenCaharacter)
    {
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
    }
}
