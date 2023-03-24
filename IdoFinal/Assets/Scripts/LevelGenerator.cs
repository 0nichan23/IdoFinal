using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level emptyLevelPrefab;


    public void GenerateLevel(Vector3Int size)
    {
        //perlin noise 2 dimentional array 
        //value between x-y -> specefic block placed
        //max value is a tree
        //min value is water


    }

}
