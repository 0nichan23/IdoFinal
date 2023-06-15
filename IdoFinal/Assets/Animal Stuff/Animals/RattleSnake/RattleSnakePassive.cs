using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RattleSnakePassive", menuName = "Passives/RattleSnake")]
public class RattleSnakePassive : AnimalPassive
{
    //critical hits inflict poison 
    [SerializeField] private float duration;
    [SerializeField] private float totalAmount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(PoisonOnCrit);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.RemoveListener(PoisonOnCrit);
    }

    private void PoisonOnCrit(AnimalAttack attack, Damageable target, DamageDealer dealer)
    {
        target.RefCharacter.Effectable.AddStatus(new Poison(duration, totalAmount), dealer);
    }
}
