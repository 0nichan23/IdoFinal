using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //curernt position of type tiledata
    //move to another tile -> add the vector read from input manager to the tiledata pos, 
    //check if there is a traversable tile in that position
    //lerp animal to tile over a given time and play walk animation

    private TileData currentTile;
    [SerializeField] private bool canMove;
    public TileData CurrentTile { get => currentTile; }


  /*  private void Start()
    {
        canMove = true;
    }*/
    private void Update()
    {
        if (canMove)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector3Int to = currentTile.GetPos + GameManager.Instance.InputManager.GetMoveVector();
        if (GameManager.Instance.InputManager.GetMoveVector() == Vector3.zero)
        {
            return;
        }
        TileData destTile = GameManager.Instance.LevemManager.CurrentLevel.GetTile(to);
        if (!ReferenceEquals(destTile, null))
        {
            StartCoroutine(MovePlayerTo(destTile.GetStandingPos));
            //transform.position = destTile.GetStandingPos;
            currentTile = destTile;
        }
    }

    private IEnumerator MovePlayerTo(Vector3 worldPos)
    {
        canMove = false;
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, worldPos, counter);
            transform.position = positionLerp;
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canMove = true;

    }

    public void SetCurrentTile(TileData tile)
    {
        currentTile = tile;
        transform.position = tile.GetStandingPos;
    }

}
