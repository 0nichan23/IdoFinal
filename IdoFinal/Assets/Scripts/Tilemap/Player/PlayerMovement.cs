using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TileData currentTile;
    [SerializeField] private bool canMove;
    public TileData CurrentTile { get => currentTile; }

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
            currentTile = destTile;
            RotatePlayerToMoveDirection(GameManager.Instance.InputManager.GetMoveVector());
        }
    }

    private IEnumerator MovePlayerTo(Vector3 worldPos)
    {
        canMove = false;
        Vector3 startPosition = transform.position;
        float counter = 0;
        GameManager.Instance.PlayerWrapper.PlayerAnimationHandler.StartWalkAnim();
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, worldPos, counter);
            transform.position = positionLerp;
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canMove = true;
        GameManager.Instance.PlayerWrapper.PlayerAnimationHandler.EndWalkAnim();
    }

    public void SetCurrentTile(TileData tile)
    {
        currentTile = tile;
        transform.position = tile.GetStandingPos;
    }

    private void RotatePlayerToMoveDirection(Vector3Int givenDir)
    {
        float yRotation = 0;

        if (givenDir.z == -1)
        {
            yRotation = 180;
        }
        else if (givenDir.x == 1)
        {
            yRotation = 90;
        }
        else if (givenDir.x == -1)
        {
            yRotation = -90;
        }

        GameManager.Instance.PlayerWrapper.Gfx.eulerAngles = new Vector3(0, yRotation, 0);
    }

}
