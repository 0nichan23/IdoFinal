using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BoarPassive", menuName = "Passives/Boar")]
public class BoarPassive : AnimalPassive
{
    //gain more resources for every kill - drop system not yet implementer
    public override void SubscribePassive(Character givenCaharacter)
    {
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
    }
}
