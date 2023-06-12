using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcticFoxPassive", menuName = "Passives/ArcticFox")]

public class ArcticFoxPassive : AnimalPassive
{
    //higher dodge chance after dealing damage to ice creatures 
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

    private void AddDodgeChanceBuff(Damageable target, AnimalAttack attack, DamageDealer dealer)
    {
        if (target.RefAnimal.Habitat == Habitat.Arctic)
        {
            dealer.RefCharacter.Effectable.AddStatus(new DodgeChanceBuff(duration, amount), dealer);
            Debug.Log("added dodge chance for 5s");
        }
    }
}
