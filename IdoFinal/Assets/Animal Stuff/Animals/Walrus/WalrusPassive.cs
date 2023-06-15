using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalrusPassive", menuName = "Passives/Walrus")]

public class WalrusPassive : AnimalPassive
{
    //every 10 attacks, gain a damage bonus for the next attack based on defense
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(empower10thAttack);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(empower10thAttack);
    }

    private void empower10thAttack(Damageable target, AnimalAttack attack, DamageDealer dealer)
    {
        if (dealer.RefCharacter.Counter.CurrentCounter == 10)
        {
            attack.Damage.AddMod(1 + dealer.RefCharacter.Damageable.DamageReduction);
            //no need to max it as it is automatic
        }
    }

}
