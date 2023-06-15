using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoyotePassive", menuName = "Passives/Coyote")]
public class CoyotePassive : AnimalPassive
{
    //gain a small amount of true healing when gaining resources 

    public override void SubscribePassive(Character givenCaharacter)
    {
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
    }
}
