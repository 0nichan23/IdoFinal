using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BigHornPassive", menuName = "Passives/BigHorn")]
public class BigHornPassive : AnimalPassive
{
    //gain attack speed and armor pen in mountains
    [SerializeField, Range(0, 10)] private int armorPenAmount;
    [SerializeField, Range(0, 1)] private float speedAmount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.AddListener(AddStats);
        givenCaharacter.OnExitLevel.AddListener(DecreaseStats);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.RemoveListener(AddStats);
        givenCaharacter.OnExitLevel.RemoveListener(DecreaseStats);
    }

    private void AddStats(Level level, Character character)
    {
        if (level.Habitat == Habitat.Mountain)
        {
            character.DamageDealer.AddArmorPenetration(armorPenAmount);
            character.AddAttackSpeed(speedAmount);
        }
    }
    private void DecreaseStats(Level level, Character character)
    {
        if (level.Habitat == Habitat.Mountain)
        {
            character.DamageDealer.AddArmorPenetration(-armorPenAmount);
            character.AddAttackSpeed(-speedAmount);
        }
    }
}
