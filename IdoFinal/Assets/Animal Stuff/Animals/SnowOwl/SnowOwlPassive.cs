using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SnowyOwlPassive", menuName = "Passives/SnowyOwl")]

public class SnowOwlPassive : AnimalPassive
{
    //crit strikes inflict bleed and deal extra damage to mammels and birds
    [SerializeField, Range(0,1)] private float damageMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(BleedAndExtraCritDamageOnMammelsAndBirds);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        throw new System.NotImplementedException();
    }

    private void BleedAndExtraCritDamageOnMammelsAndBirds(AnimalAttack attack, Damageable target)
    {
        if (target.RefAnimal.AnimalClass == AnimalClass.Mammel || target.RefAnimal.AnimalClass == AnimalClass.Bird)
        {
            attack.Damage.AddMod(1 + damageMod);
           // target.RefCharacter.Effectable.AddStatus;
        }
    }
}
