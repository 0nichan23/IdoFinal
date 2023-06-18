using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmadilloPassive", menuName = "Passives/Armadillo")]
public class ArmadilloPassive : AnimalPassive
{

    //chance to block incomming damage
    [SerializeField, Range(0, 100)] private float chacne;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(BlockIncommingDamage);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(BlockIncommingDamage);
    }

    private void BlockIncommingDamage(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0,100) <= chacne)
        {
            dmg.AddMod(0);
        }
    }
}
