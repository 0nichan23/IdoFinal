using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TapirPassive", menuName = "Passives/Tapir")]

public class TapirPassive : AnimalPassive
{
    //damage reducion when swimming
    public override void SubscribePassive(Character givenCaharacter)
    {
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
    }
}
