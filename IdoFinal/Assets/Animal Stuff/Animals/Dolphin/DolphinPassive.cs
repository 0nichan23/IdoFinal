using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DolphinPassive", menuName = "Passives/Dolphin")]
public class DolphinPassive : AnimalPassive
{
    //extra crit chance while swimming
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnterdWater.AddListener(AddCritChance);
        givenCaharacter.OnExitWater.AddListener(RemoveCritChance);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnterdWater.RemoveListener(AddCritChance);
        givenCaharacter.OnExitWater.RemoveListener(RemoveCritChance);
    }

    private void AddCritChance(Character character)
    {
        character.DamageDealer.AddCritChance(amount);
    }

    private void RemoveCritChance(Character character)
    {
        character.DamageDealer.AddCritChance(-amount);
    }
}
