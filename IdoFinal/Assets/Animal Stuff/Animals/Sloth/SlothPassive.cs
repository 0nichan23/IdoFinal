using UnityEngine;

[CreateAssetMenu(fileName = "SlothPassive", menuName = "Passives/Sloth")]
public class SlothPassive : AnimalPassive
{
    //the lower attack speed is (down to a min) the more damage dealt up to a max
    [SerializeField, Range(0, 1)] private float maxDamage;
    [SerializeField, Range(-0.9f, 0.9f)] private float minAttackSpeed;

    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(IncreaseDamage);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(IncreaseDamage);
    }
    private void IncreaseDamage(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        //min attack speed => max damage
        //-0.9 => 0.5 => 1.5mod
        //-0.7 => -0.7 - -0.9 =>0.2=> mod -= mod *  0.2
        float damageMod = maxDamage;
        float spd = dealer.RefCharacter.AttackSpeed - minAttackSpeed;
        damageMod -= damageMod * spd;
        dmg.AddMod(1 + damageMod);
    }
}
