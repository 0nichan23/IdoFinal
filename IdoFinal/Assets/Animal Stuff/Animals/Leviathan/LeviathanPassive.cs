using UnityEngine;


[CreateAssetMenu(fileName = "LeviathanPassive", menuName = "Passives/Leviathan")]
public class LeviathanPassive : AnimalPassive
{
    //deal more and take less damage in the water, critial hits apply attack speed debuff
    [SerializeField, Range(0, 1)] private float waterDamageMod;
    [SerializeField, Range(0, 1)] private float waterDamageReducion;
    [SerializeField, Range(0, 10)] private float debuffDuration;
    [SerializeField, Range(0, 1)] private float debuffMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(ApplyAttackSpeedDebuff);
        givenCaharacter.DamageDealer.OnHit.AddListener(RaiseDamageInWater);
        givenCaharacter.Damageable.OnGetHit.AddListener(ReduceDamageInWater);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.RemoveListener(ApplyAttackSpeedDebuff);
        givenCaharacter.DamageDealer.OnHit.RemoveListener(RaiseDamageInWater);
        givenCaharacter.Damageable.OnGetHit.RemoveListener(ReduceDamageInWater);

    }

    private void ApplyAttackSpeedDebuff(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        target.RefCharacter.Effectable.AddStatus(new AttackSpeedDebuff(debuffDuration, debuffMod), dealer);
    }

    private void RaiseDamageInWater(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (dealer.RefCharacter.MovementMode == MovementMode.Water)
        {
            dmg.AddMod(1 + waterDamageMod);
        }
    }

    private void ReduceDamageInWater(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefCharacter.MovementMode == MovementMode.Water)
        {
            dmg.AddMod(1 - waterDamageMod);
        }
    }

}
