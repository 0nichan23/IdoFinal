using UnityEngine;

[CreateAssetMenu(fileName = "SnappingTurtlePassive", menuName = "Passives/SnappingTurtle")]
public class SnappingTurtlePassive : AnimalPassive
{
    //every time you take damage, empower the next attack damage
    [SerializeField, Range(0, 1)] private float damageMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.AddListener(SubscribeDamageMod);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.Damageable.OnGetHit.RemoveListener(SubscribeDamageMod);
    }

    private void SubscribeDamageMod(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        target.RefCharacter.DamageDealer.OnHit.AddListener(DamageMod);
    }

    private void DamageMod(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        dmg.AddMod(1 + damageMod);
        dealer.OnHit.RemoveListener(DamageMod);
    }
}
