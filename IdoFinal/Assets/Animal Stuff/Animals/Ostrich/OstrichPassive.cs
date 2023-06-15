using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OstrichPassive", menuName = "Passives/Ostrich")]
public class OstrichPassive : AnimalPassive
{
    [SerializeField, Range(0,1)] private float dodgeChance;
    //add dodge chance 
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.AddListener(AddStats);
        givenCaharacter.OnExitLevel.AddListener(ReduceStats);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.RemoveListener(AddStats);
        givenCaharacter.OnExitLevel.RemoveListener(ReduceStats);
    }

    private void AddStats(Level level, Character character)
    {
        if (level.Habitat == Habitat.Desert || level.Habitat == Habitat.Plains)
        {
            character.Damageable.AddDodgeChance(dodgeChance);
        }
    }
    private void ReduceStats(Level level, Character character)
    {
        if (level.Habitat == Habitat.Desert || level.Habitat == Habitat.Plains)
        {
            character.Damageable.AddDodgeChance(-dodgeChance);
        }
    }

}
