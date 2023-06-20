using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrcaPassive", menuName = "Passives/Orca")]
public class OrcaPassive : AnimalPassive
{
    //Gain a damage and attack speed buff on kill
    [SerializeField, Range(0, 10)] private float attackDuration;
    [SerializeField, Range(0, 1)] private float attackMod;
    [SerializeField, Range(0, 10)] private float aspeedDuration;
    [SerializeField, Range(0, 1)] private float aspeedMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnKill.AddListener(ApplyBuffs);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnKill.RemoveListener(ApplyBuffs);
    }

    private void ApplyBuffs(Damageable target, DamageDealer dealer)
    {
        dealer.RefCharacter.Effectable.AddStatus(new DamageBuff(attackDuration, attackMod), dealer);
        dealer.RefCharacter.Effectable.AddStatus(new AttackSpeedBuff(aspeedDuration, aspeedMod), dealer);
    }
}
