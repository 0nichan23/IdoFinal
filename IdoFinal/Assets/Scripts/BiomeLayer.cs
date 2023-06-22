using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiomeHeightData", menuName = "Biome")]
public class BiomeLayer : ScriptableObject
{
    [SerializeField] private List<BiomeHeightData> heightData = new List<BiomeHeightData>();
    [SerializeField] private EnemyCreator enemyCreator;
    [SerializeField] private BiomeDropHandler dropHandler;
    [SerializeField] private MyTile encasingBlock;
    public EnemyCreator EnemyCreator { get => enemyCreator; }

    public MyTile GetTileFromHeight(float height)
    {
        BiomeHeightData closesMaxvalue = heightData[0];
        if (height <= closesMaxvalue.MaxHeight)
        {
            return closesMaxvalue.RefTile;
        }
        for (int i = 0; i < heightData.Count; i++)
        {
            if (height > heightData[i].MaxHeight)
            {
                continue;
            }
            closesMaxvalue = heightData[i];
            break;
        }
        return closesMaxvalue.RefTile;
    }
    
    public List<BiomeHeightData> HeightData { get => heightData; }
    public BiomeDropHandler DropHandler { get => dropHandler;  }
    public MyTile EncasingBlock { get => encasingBlock;  }
}


[System.Serializable]
public class BiomeHeightData
{
    public MyTile RefTile;
    public float MaxHeight;
}
