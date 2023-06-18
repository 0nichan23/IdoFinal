using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RhinoPassive", menuName = "Passives/Rhino")]

public class RhinoPassive : AnimalPassive
{
    //chance to ignore damage from birds
    [SerializeField, Range(0,100)] private float chance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(IgnoreDamageFromBirds);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(IgnoreDamageFromBirds);
    }

    private void IgnoreDamageFromBirds(AnimalAttack attack, Damageable target, DamageDealer attacker, DamageHandler dmg)
    {
        if (attacker.RefAnimal.AnimalClass == AnimalClass.Bird)
        {
            if (Random.Range(0,100) <= chance)
            {
                dmg.AddMod(0f);
            }
        }
    }
}
