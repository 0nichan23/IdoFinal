using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KoalaPassive", menuName = "Passives/Koala")]

public class KoalaPassive : AnimalPassive
{
    //gain true heal proportionate to the damage dealt in any forest or jungle
    [SerializeField, Range(0, 1)] private float damageHealed;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealDamageFinal.AddListener(HealOnHit);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealDamageFinal.RemoveListener(HealOnHit);

    }

    private void HealOnHit(AnimalAttack attack, Damageable target, DamageDealer dealer)
    {
        if (GameManager.Instance.LevelManager.CurrentLevel.Habitat == Habitat.Forest || GameManager.Instance.LevelManager.CurrentLevel.Habitat == Habitat.Jungle)
        {
            dealer.RefCharacter.Damageable.HealTrueDamage(attack.Damage.CalcFinalDamageMult() * damageHealed);
        }
    }


}
