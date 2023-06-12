using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class PopupSpawner : MonoBehaviour
{
    [SerializeField] private DamageNumberMesh damagePopup;
    [SerializeField] private DamageNumberMesh criticalDamagePopup;


    public void SpawnDamagePopup(Vector3 position, float amount)
    {
        DamageNumber number = damagePopup.Spawn(position, amount);
    }
    public void SpawnCritDamagePopup(Vector3 position, float amount)
    {
        DamageNumber number = criticalDamagePopup.Spawn(position, amount);
    }
}
