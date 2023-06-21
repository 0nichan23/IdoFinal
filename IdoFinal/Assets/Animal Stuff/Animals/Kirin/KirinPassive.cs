using UnityEngine;


[CreateAssetMenu(fileName = "KirinPassive", menuName = "Passives/Kirin")]
public class KirinPassive : AnimalPassive
{
    //Kirin has enhanced crit chance and multi hits stunned targets 
    [SerializeField, Range(0, 1)] private float flatCritChance;
    [SerializeField, Range(0, 5)] private int extraHits;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.AddCritChance(flatCritChance);
        givenCaharacter.DamageDealer.OnDealDamageFinal.AddListener(MultiHitStunnedTargets);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.AddCritChance(-flatCritChance);
        givenCaharacter.DamageDealer.OnDealDamageFinal.RemoveListener(MultiHitStunnedTargets);
    }

    private void MultiHitStunnedTargets(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        foreach (var item in target.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Stun)
            {
                for (int i = 0; i < extraHits; i++)
                {
                    target.GetMultiHit(dealer, dmg.CalcFinalDamageMult());
                }
            }
        }

    }
}
