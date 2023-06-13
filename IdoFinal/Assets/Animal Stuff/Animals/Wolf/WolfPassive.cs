using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WolfPassive", menuName = "Passives/Wolf")]
public class WolfPassive : AnimalPassive
{
    //deal more damage the faster the enemy is up to a maximum
    [SerializeField, Range(0,1)] private float maximumDamageMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(AddDamageBasedOnEnemySpeed);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(AddDamageBasedOnEnemySpeed);
    }

    private void AddDamageBasedOnEnemySpeed(Damageable target, AnimalAttack attack, DamageDealer dealer)
    {
        float damageMod = 0f;
        for (int i = 0; i < target.RefAnimal.StatSheet.Speed; i++)
        {
            damageMod += (maximumDamageMod / 10);
        }
        attack.Damage.AddMod(1 + damageMod);
    }
}
