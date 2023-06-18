using UnityEngine;

[CreateAssetMenu(fileName = "GorillaPassive", menuName = "Passives/Gorilla")]
public class GorillaPassive : AnimalPassive
{
    //deal more damage to stunned targets 
    [SerializeField, Range(0, 1)] private float damageMod;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(DamageIncrease);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(DamageIncrease);

    }

    private void DamageIncrease(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        foreach (var item in target.RefCharacter.Effectable.ActiveEffects)
        {
            if (item is Stun)
            {
                dmg.AddMod(1 + damageMod);
            }
        }
    }
}
