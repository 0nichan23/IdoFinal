using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WombatPassive", menuName = "Passives/Wombat")]
public class WombatPassive : AnimalPassive
{
    //chance to inflict poison when taking damage from a carnivore
    [SerializeField, Range(0, 100)] private float chacne;
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 100)] private float totalAmount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(PoisonCarnivore);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(PoisonCarnivore);
    }

    private void PoisonCarnivore(AnimalAttack attack, Damageable target, DamageDealer dealer)
    {
        if (target.RefAnimal.Diet == Diet.Carnivore)
        {
            if (Random.Range(0,100) <= chacne)
            {
                dealer.RefCharacter.Effectable.AddStatus(new Poison(duration, totalAmount), target.RefCharacter.DamageDealer);
            }
        }
    }
}
