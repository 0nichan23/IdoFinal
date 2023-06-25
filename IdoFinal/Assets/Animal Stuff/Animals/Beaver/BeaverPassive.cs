using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeaverPassive", menuName = "Passives/Beaver")]
public class BeaverPassive : AnimalPassive
{
    //deal more damage the more trees you have around you
    [SerializeField, Range(0, 1)] private float damagePerTree;
    [SerializeField, Range(0, 10)] private int radius;
    private ProximitySensor sensor = new ProximitySensor();

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
        List<TileData> tilesInRad = sensor.GetTilesInRadius(radius, dealer.RefCharacter.CurrentTile, GameManager.Instance.LevelManager.CurrentLevel.ObstacleMap);
        float mod = 0;
        foreach (var item in tilesInRad)
        {
            if (item.GetObj.CompareTag("Tree"))
            {
                mod += damagePerTree;
            }
        }
        dmg.AddMod(1 + mod);
    }



}
