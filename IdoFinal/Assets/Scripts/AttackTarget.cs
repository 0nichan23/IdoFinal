using System.Collections.Generic;
using UnityEngine;

public class AttackTarget
{
    public void AttackTiles(LookDirections direction ,Vector3Int originPos, AnimalAttack givenAttack, Character dealer)
    {
        List<Vector3Int> finalPositions = new List<Vector3Int>();

        foreach (var item in givenAttack.Hitbox)
        {
            Vector3Int newPos = new Vector3Int();
            switch (direction)
            {
                case LookDirections.UP:
                    newPos = new Vector3Int(item.z * -1, 0, item.x);
                    newPos += originPos;
                    break;
                case LookDirections.DOWN:
                    newPos = new Vector3Int(item.z, 0, item.x * -1);
                    newPos += originPos;
                    break;
                case LookDirections.LEFT:
                    newPos = originPos - item;
                    break;
                case LookDirections.RIGHT:
                    newPos = originPos + item;
                    break;
            }
            finalPositions.Add(newPos);
        }
        GameManager.Instance.LevelManager.DealDamageOnTiles(finalPositions, givenAttack, dealer);
    }




}
