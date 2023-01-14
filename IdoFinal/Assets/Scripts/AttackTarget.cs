using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    //wait for the player to select a direction to attack in,
    //based on the range of the attack mark the tiles that will be hit
    //can only hit traversable tiles obv

    public void AttackTiles(AnimalAttack givenAttack, DamageDealer dealer = null)
    {
        //get the directio the player is looking at
        List<Vector3Int> finalPositions = new List<Vector3Int>();

        foreach (var item in givenAttack.Hitbox)
        {
            Vector3Int newPos = new Vector3Int();
            switch (GameManager.Instance.PlayerWrapper.PlayerMovement.LookingTowards)
            {
                case LookDirections.UP:
                    newPos = new Vector3Int((GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.y + item.y) * -1, (GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.x + item.x));
                    break;
                case LookDirections.DOWN:
                    newPos = new Vector3Int((GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.y + item.y) , (GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.x + item.x) * -1);

                    break;
                case LookDirections.LEFT:
                    newPos = new Vector3Int((GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.x + item.x) * -1, (GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.y + item.y) * -1);

                    break;
                case LookDirections.RIGHT:
                    newPos = new Vector3Int((GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.x + item.x) , (GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos.y + item.y));
                    break;
            }

            finalPositions.Add(newPos);
        }
        GameManager.Instance.LevemManager.DealDamageOnTiles(finalPositions, givenAttack, dealer);
        //level manager will manage tile damaging
    }


  

}
