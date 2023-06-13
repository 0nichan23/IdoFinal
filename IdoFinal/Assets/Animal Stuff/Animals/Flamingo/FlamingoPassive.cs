using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FlamingoPassive", menuName = "Passives/Flamingo")]
public class FlamingoPassive : AnimalPassive
{
    //gain regeneration after killing fresh water creatures
    [SerializeField, Range(5,10)] private float duration;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnKill.AddListener(GainRegenOnKill);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnKill.RemoveListener(GainRegenOnKill);
    }


    private void GainRegenOnKill(Damageable target, DamageDealer dealer)
    {
        if (target.RefAnimal.Habitat == Habitat.FreshWater)
        {
            dealer.RefCharacter.Effectable.AddStatus(new Regeneration(duration), dealer);
        }
    }
}
