using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PolarBearPassive", menuName = "Passives/PolarBear")]

public class PolarBearPassive : AnimalPassive
{
    //polar bear recieves damage and crit chance buff if the active level is a snow biome

    [SerializeField] private float damageBuff;
    [SerializeField] private float critChance;
    //no level system yet. when the system is implemented, subscribe to on level enter and exit to add or remove buffs.
    public override void SubscribePassive(Character givenCaharacter)
    {
        GameManager.Instance.PlayerWrapper.DamageDealer.AddCritChance(critChance);
        GameManager.Instance.PlayerWrapper.DamageDealer.AddAttackDamage(damageBuff);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        GameManager.Instance.PlayerWrapper.DamageDealer.AddCritChance(-critChance);
        GameManager.Instance.PlayerWrapper.DamageDealer.AddAttackDamage(-damageBuff);
    }
}
