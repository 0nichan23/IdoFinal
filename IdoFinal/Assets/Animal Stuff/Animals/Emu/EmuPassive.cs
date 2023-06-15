using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmuPassive", menuName = "Passives/Emu")]
public class EmuPassive : AnimalPassive
{
    //accuracy bonus in desert plains and forests
    [SerializeField, Range(0, 1)] private float amount; 
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
        if (level.Habitat == Habitat.Forest || level.Habitat == Habitat.Desert || level.Habitat == Habitat.Plains)
        {
            character.DamageDealer.AddHitChance(amount);
        }
    }

    private void ReduceStats(Level level, Character character)
    {
        if (level.Habitat == Habitat.Forest || level.Habitat == Habitat.Desert || level.Habitat == Habitat.Plains)
        {
            character.DamageDealer.AddHitChance(-amount);
        }
    }
}
