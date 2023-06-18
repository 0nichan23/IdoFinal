using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcticFoxPassive", menuName = "Passives/ArcticFox")]

public class ArcticFoxPassive : AnimalPassive
{
    //dodge chance buff after hitting arctic creatures
    [SerializeField] private float duration;
    [SerializeField, Range(0,1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(AddDodgeChanceBuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(AddDodgeChanceBuff);
    }

    private void AddDodgeChanceBuff(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefAnimal.Habitat == Habitat.Arctic)
        {
            dealer.RefCharacter.Effectable.AddStatus(new DodgeChanceBuff(duration, amount), dealer);
        }
    }
}
