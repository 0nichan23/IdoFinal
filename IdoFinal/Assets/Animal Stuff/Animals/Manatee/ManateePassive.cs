using UnityEngine;

[CreateAssetMenu(fileName = "ManateePassive", menuName = "Passives/Manatee")]
public class ManateePassive : AnimalPassive
{
    //gain an attack speed buff when entering water
    [SerializeField, Range(0, 10)] private float duration;
    [SerializeField, Range(0, 1)] private float amount;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnterdWater.AddListener(ApplyAttackSpeedBuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnterdWater.RemoveListener(ApplyAttackSpeedBuff);
    }

    private void ApplyAttackSpeedBuff(Character character)
    {
        character.Effectable.AddStatus(new AttackSpeedBuff(duration, amount), character.DamageDealer);
    }
}
