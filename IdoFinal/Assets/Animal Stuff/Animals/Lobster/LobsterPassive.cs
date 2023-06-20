using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LobsterPassive", menuName = "Passives/Lobster")]
public class LobsterPassive : AnimalPassive
{
    //chance to apply a defense debuff when dealing damage
    [SerializeField, Range(0, 100)] private float chance;
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(ApplyDefenseDebuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(ApplyDefenseDebuff);
    }

    private void ApplyDefenseDebuff(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0,100) <= chance)
        {
            target.RefCharacter.Effectable.AddStatus(new DefenseDebuff(duration, amount), dealer);
        }
    }
}
