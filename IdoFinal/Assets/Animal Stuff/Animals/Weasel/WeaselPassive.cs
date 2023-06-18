using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaselPassive", menuName = "Passives/Weasel")]

public class WeaselPassive : AnimalPassive
{
    //inflict bleed on every 3rd attack (kamaitachi reference)
    [SerializeField, Range(5, 10)] private float duration;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(InflictBleedOnEvery3rdAttack);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(InflictBleedOnEvery3rdAttack);

    }

    private void InflictBleedOnEvery3rdAttack(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefCharacter.Counter.CurrentCounter %3 == 0)
        {
            target.RefCharacter.Effectable.AddStatus(new Bleed(duration), dealer);
        }
    }
}
