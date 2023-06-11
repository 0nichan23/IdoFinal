using System.Collections;
using UnityEngine;


public enum LookDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class PlayerMovement : MonoBehaviour
{
    private TileData currentTile;
    [SerializeField] private bool canMove;
    [SerializeField] private float movementMod = 1;
    [SerializeField] private LookDirections lookingTowards;
    public TileData CurrentTile { get => currentTile; }
    public LookDirections LookingTowards { get => lookingTowards; }

    private void Start()
    {
        GameManager.Instance.InputManager.OnTurnLeft.AddListener(TurnLeft);
        GameManager.Instance.InputManager.OnTurnLeft.AddListener(SetLookDir);
        GameManager.Instance.InputManager.OnTurnRight.AddListener(TurnRight);
        GameManager.Instance.InputManager.OnTurnRight.AddListener(SetLookDir);
    }

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
        if (GameManager.Instance.InputManager.GetMoveVector() == Vector3.zero || (GameManager.Instance.InputManager.GetMoveVector().x != 0 && GameManager.Instance.InputManager.GetMoveVector().z != 0))
        {
            return;
        }
        TileData destTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(to);
        if (!ReferenceEquals(destTile, null) && !destTile.Occupied)
        {
            currentTile.UnSubscribeCharacter();
            StartCoroutine(MovePlayerTo(destTile.GetStandingPos));
            currentTile = destTile;
            destTile.SubscribeCharacter(GameManager.Instance.PlayerWrapper);
            RotatePlayerToMoveDirection(GameManager.Instance.InputManager.GetMoveVector());
        }
    }

    private void TurnRight()
    {
        GameManager.Instance.PlayerWrapper.Gfx.eulerAngles = new Vector3(0, GameManager.Instance.PlayerWrapper.Gfx.eulerAngles.y + 90, 0);
    }

    private void TurnLeft()
    {
        GameManager.Instance.PlayerWrapper.Gfx.eulerAngles = new Vector3(0, GameManager.Instance.PlayerWrapper.Gfx.eulerAngles.y - 90, 0);
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
            counter += Time.deltaTime * movementMod;
            yield return new WaitForEndOfFrame();
        }
        canMove = true;
        GameManager.Instance.PlayerWrapper.PlayerAnimationHandler.EndWalkAnim();
    }

    public void SetCurrentTile(TileData tile)
    {
        currentTile = tile;
        tile.SubscribeCharacter(GameManager.Instance.PlayerWrapper);
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
        SetLookDir();
    }

    private void SetLookDir()
    {
        switch (Mathf.FloorToInt(GameManager.Instance.PlayerWrapper.Gfx.eulerAngles.y))
        {
            case 270:
                lookingTowards = LookDirections.LEFT;
                break;
            case 0:
                lookingTowards = LookDirections.UP;
                break;
            case 180:
                lookingTowards = LookDirections.DOWN;
                break;
            case 90:
                lookingTowards = LookDirections.RIGHT;
                break;
        }
    }

    public void ResetCanMove()
    {
        canMove = true;
    }
}
