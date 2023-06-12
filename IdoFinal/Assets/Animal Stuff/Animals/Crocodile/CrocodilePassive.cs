using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrocodilePassive", menuName = "Passives/Crocodile")]

public class CrocodilePassive : AnimalPassive
{
    //add crit damage on aquatic creatures
    [SerializeField] private float DamageMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(CritDamageOnAquaticCreatures);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.RemoveListener(CritDamageOnAquaticCreatures);
    }

    private void CritDamageOnAquaticCreatures(AnimalAttack attack, Damageable target, DamageDealer dealer)
    {
        if (target.RefAnimal.Habitat == Habitat.FreshWater || target.RefAnimal.Habitat == Habitat.SaltWater)
        {
            attack.Damage.AddMod(DamageMod);
        }
    }
}
