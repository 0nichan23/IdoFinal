using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OxPassive", menuName = "Passives/Ox")]

public class OxPassive : AnimalPassive
{
    //immune to negative effects in arctic levels
    public override void SubscribePassive(Character givenCaharacter)
    {

        givenCaharacter.OnEnteredLevel.AddListener(SubscribeCleanse);
        givenCaharacter.OnExitLevel.AddListener(UnSubscribeCleanse);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.RemoveListener(SubscribeCleanse);
        givenCaharacter.OnExitLevel.RemoveListener(UnSubscribeCleanse);

    }
    private void SubscribeCleanse(Level level, Character character)
    {
        if (level.Habitat == Habitat.Arctic || level.Habitat == Habitat.Mountain)
        {
            character.Effectable.OnObtainEffect.AddListener(CleanseNegativeEffects);
        }
    }
    private void CleanseNegativeEffects(StatusEffect effect, Effectable target)
    {
        if (effect.EffectOri == EffectOrientation.NEG)
        {
            target.RemoveStatus(effect);
        }
    }
    private void UnSubscribeCleanse(Level level, Character character)
    {
        if (level.Habitat == Habitat.Forest || level.Habitat == Habitat.Mountain)
        {
            character.Effectable.OnObtainEffect.RemoveListener(CleanseNegativeEffects);

        }
    }

}
