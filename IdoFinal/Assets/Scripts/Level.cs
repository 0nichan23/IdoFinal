using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private List<TileData> traversableGround = new List<TileData>();
    [SerializeField] private TileData startTile;
    public UnityEvent OnDoneCreatingRoom;
    public Tilemap Tilemap { get => tilemap; }
    public List<TileData> TraversableGround { get => traversableGround; }
    public TileData StartTile { get => startTile; }

    private void Start()
    {
        StartCoroutine(StartUpLevel());
    }

    private IEnumerator StartUpLevel()
    {
        yield return new WaitForSecondsRealtime(1f);
        SetTraversableGround();
    }

    private void SetTraversableGround()
    {
        BoundsInt bounds = tilemap.cellBounds;
        //get all of the tilemap's children,
        //loop over them and check if the gameobject is walkable,
        //if it is add it to the list with its local pos as v3int as the key and the object as the value
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            Transform child = tilemap.transform.GetChild(i);
            if (child.gameObject.CompareTag("Walkable"))
            {
                Vector3Int tilePos = new Vector3Int(Mathf.FloorToInt(child.localPosition.x), 0, Mathf.FloorToInt(child.localPosition.z));
                traversableGround.Add(new TileData(tilePos, child.gameObject));
            }
        }

        foreach (var tile in traversableGround)
        {
            InteractableTile interTile = Instantiate(GameManager.Instance.InteractableTilePrefab, tile.GetObj.transform);
            interTile.transform.position = new Vector3(tile.GetStandingPos.x, tile.GetStandingPos.y + 0.2f, tile.GetStandingPos.z);
            tile.CacheOverlayObject(interTile);
            interTile.gameObject.SetActive(false);
        }

        SetStartTile();
        PlacePlayerAtStart();
        OnDoneCreatingRoom?.Invoke();
    }

    private void SetStartTile()
    {
        startTile = traversableGround[Random.Range(0, traversableGround.Count)];
    }

    private void PlacePlayerAtStart()
    {
        GameManager.Instance.PlayerWrapper.PlayerMovement.SetCurrentTile(startTile);
    }

    public TileData GetTile(Vector3Int givenPos)
    {
        foreach (var item in traversableGround)
        {
            if (item.GetPos == givenPos)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class TileData
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private InteractableTile overlay;
    [SerializeField] private Vector3Int Pos;

    public GameObject GetObj { get => Obj; }
    public Vector3Int GetPos { get => Pos; }
    public Vector3 GetStandingPos { get => new Vector3(Obj.transform.position.x, Obj.transform.position.y + 0.6f, Obj.transform.position.z); }
    public InteractableTile Overly { get => overlay; }

    public TileData(Vector3Int pos, GameObject obj)
    {
        Obj = obj;
        Pos = pos;
    }

    public void CacheOverlayObject(InteractableTile givenTile)
    {
        overlay = givenTile;
    }
    public bool ComparePositons(Vector3Int otherPos)
    {
        if (Pos.x == otherPos.x && Pos.z == otherPos.z)
        {
            return true;
        }
        return false;
    }
}

