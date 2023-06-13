using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZebraPassive", menuName = "Passives/Zebra")]
public class ZebraPassive : AnimalPassive
{
    //gain defense in plains and desert
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.AddListener(AddDefense);
        givenCaharacter.OnExitLevel.AddListener(DecreaseDefense);
    }
    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.RemoveListener(AddDefense);
        givenCaharacter.OnExitLevel.RemoveListener(DecreaseDefense);
    }

    private void AddDefense(Level level, Character character)
    {
        if (level.Habitat == Habitat.Plains || level.Habitat == Habitat.Desert)
        {
            character.Damageable.AddDamageReduction(amount);
        }
    }

    private void DecreaseDefense(Level level, Character character)
    {
        if (level.Habitat == Habitat.Plains || level.Habitat == Habitat.Desert)
        {
            character.Damageable.AddDamageReduction(-amount);
        }
    }
}
