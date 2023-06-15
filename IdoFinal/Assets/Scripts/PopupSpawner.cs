using DamageNumbersPro;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    [SerializeField] private DamageNumberMesh damagePopup;
    [SerializeField] private DamageNumberMesh criticalDamagePopup;
    [SerializeField] private DamageNumberMesh missPopup;


    public void SpawnDamagePopup(Vector3 position, float amount)
    {
        DamageNumber number = damagePopup.Spawn(position, amount, Color.white);
    }
    public void SpawnMissPopup(Vector3 position)
    {
        DamageNumber number = missPopup.Spawn(position);
    }
    public void SpawnDamagePopup(Vector3 position, float amount, Color givenColor)
    {
        DamageNumber number = damagePopup.Spawn(position, amount, givenColor);
    }
    public void SpawnCritDamagePopup(Vector3 position, float amount)
    {
        DamageNumber number = criticalDamagePopup.Spawn(position, amount, Color.red);
    }
}
