using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PolarBearPassive", menuName = "Passives/PolarBear")]

public class PolarBearPassive : AnimalPassive
{
    //polar bear recieves damage and crit chance buff if the active level is a snow biome

    [SerializeField, Range(0,1)] private float damageBuff;
    [SerializeField, Range(0, 1)] private float critChance;
    public override void SubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.AddListener(AddStats);
        givenCaharacter.OnExitLevel.AddListener(RemoveStats);
    }

    public override void UnSubscribePassive(Character givenCaharacter)
    {
        givenCaharacter.OnEnteredLevel.RemoveListener(AddStats);
        givenCaharacter.OnExitLevel.RemoveListener(RemoveStats);
    }

    private void AddStats(Level active, Character givenCaharacter)
    {
        if (active.Habitat == Habitat.Arctic)
        {
            givenCaharacter.DamageDealer.AddAttackDamage(givenCaharacter.DamageDealer.PowerDamageMod * damageBuff);
            givenCaharacter.DamageDealer.AddCritChance(critChance);
        }
    }

    private void RemoveStats(Level active, Character givenCaharacter)
    {
        if (active.Habitat == Habitat.Arctic)
        {
            givenCaharacter.DamageDealer.AddAttackDamage(-(givenCaharacter.DamageDealer.PowerDamageMod * damageBuff));
            givenCaharacter.DamageDealer.AddCritChance(-critChance);
        }
    }
}
