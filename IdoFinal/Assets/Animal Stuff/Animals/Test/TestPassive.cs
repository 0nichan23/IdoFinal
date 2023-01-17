using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Animal", menuName = "Passives/Test")]
public class TestPassive : AnimalPassive
{
    public override void SubscribePassive()
    {
        throw new System.NotImplementedException();
    }

    public override void UnSubscribePassive()
    {
        throw new System.NotImplementedException();
    }
}
