using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyCreator", menuName = "EnemyCreator")]

public class EnemyCreator : ScriptableObject
{
    [SerializeField] private List<BiomeSpawnData> spawnData = new List<BiomeSpawnData>();

    public Animal GetEnemyAnimalFromValue(float value)
    {
        BiomeSpawnData closesMaxvalue = SpawnData[0];
        if (value <= closesMaxvalue.MaxChacne)
        {
            return closesMaxvalue.AnimalToSpawn;
        }
        for (int i = 0; i < SpawnData.Count; i++)
        {
            if (value > SpawnData[i].MaxChacne)
            {
                continue;
            }
            closesMaxvalue = SpawnData[i];
            break;
        }
        return closesMaxvalue.AnimalToSpawn;
    }
    public List<BiomeSpawnData> SpawnData { get => spawnData;}

}
[System.Serializable]
public struct BiomeSpawnData
{
    public float MaxChacne;
    public Animal AnimalToSpawn;
}