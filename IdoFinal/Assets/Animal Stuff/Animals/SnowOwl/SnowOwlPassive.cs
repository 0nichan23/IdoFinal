using UnityEngine;

[CreateAssetMenu(fileName = "SnowyOwlPassive", menuName = "Passives/SnowyOwl")]

public class SnowOwlPassive : AnimalPassive
{
    //crit strikes inflict bleed and deal extra damage to mammels and birds
    [SerializeField, Range(0, 1)] private float damageMod;
    [SerializeField, Range(5, 10)] private float bleedDuration;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.AddListener(BleedAndExtraCritDamageOnMammelsAndBirds);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnDealCritDamage.RemoveListener(BleedAndExtraCritDamageOnMammelsAndBirds);

    }

    private void BleedAndExtraCritDamageOnMammelsAndBirds(AnimalAttack attack, Damageable target, DamageDealer dealer, DamageHandler dmg)
    {
        if (target.RefAnimal.AnimalClass == AnimalClass.Mammel || target.RefAnimal.AnimalClass == AnimalClass.Bird)
        {
            dmg.AddMod(1 + damageMod);
            target.RefCharacter.Effectable.AddStatus(new Bleed(bleedDuration), dealer);
        }
    }
}
