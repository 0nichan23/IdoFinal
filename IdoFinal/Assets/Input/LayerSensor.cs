using UnityEngine;
using System.Collections.Generic;
using System;

public class LayerSensor : MonoBehaviour
{
    [SerializeField] private LayerMask layerToCheck;
    [SerializeField] private List<Sensor> sensorList = new List<Sensor>();

    // add offset to object position, find direction of ray from position, draw using size of range

    public bool IsTouching()
    {
        for (int i = 0; i < sensorList.Count ; i++)
        {
            Vector2 startPosition = new Vector2(transform.position.x + sensorList[i].Offset.x, transform.position.y + sensorList[i].Offset.y);
            if (Physics.Raycast(startPosition, sensorList[i].Direction, sensorList[i].Range, layerToCheck))
            {
                return true;
            }
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        foreach (var item in sensorList)
        {
            Vector2 startPos = new Vector2(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            Vector2 dir = item.Direction * item.Range;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(startPos, dir);
        }



    }


}

[System.Serializable]
public class Sensor
{
    public float Range;
    public Vector2 Offset;
    public Vector2 Direction;
}