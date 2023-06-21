using UnityEngine;

[CreateAssetMenu(fileName = "WyvernPassive", menuName = "Passives/Wyvern")]
public class WyvernPassive : AnimalPassive
{
    //all attacks have a chance to inflict poison
    [SerializeField, Range(0, 10)] private float poisonDuration;
    [SerializeField, Range(0, 100)] private float poisonTotalAmount;
    [SerializeField, Range(0, 100)] private float poisonChance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.AddListener(Poison);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.DamageDealer.OnHit.RemoveListener(Poison);
    }

    private void Poison(Damageable target, AnimalAttack attack, DamageDealer dealer, DamageHandler dmg)
    {
        if (Random.Range(0, 100) <= poisonChance)
        {
            target.RefCharacter.Effectable.AddStatus(new Poison(poisonDuration, poisonTotalAmount), dealer);
        }
    }
}
