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
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private Habitat habitat;
    [SerializeField] private bool setupOnAwake;
    private EnemyCreator enemyCreator;
    public Tilemap Tilemap { get => tilemap; }
    public List<TileData> TraversableGround { get => traversableGround; }
    public TileData StartTile { get => startTile; }
    public List<Enemy> Enemies { get => enemies; }
    public Habitat Habitat { get => habitat; set => habitat = value; }
    public EnemyCreator EnemyCreator { get => enemyCreator; set => enemyCreator = value; }

    public void SetUpLevel()
    {
        SetTraversableGround();
        SpawnEnemy();
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


        OnDoneCreatingRoom?.Invoke();
    }

    public List<TileData> GetNeighbours(TileData givenTile)
    {
        List<TileData> validNeighbours = new List<TileData>();
        for (int x = -1; x <= 1; x++)
        {
            if (x == 0)
            {
                continue;
            }
            Vector3Int tilePos = givenTile.GetPos + new Vector3Int(x, 0, 0);
            TileData newTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tilePos);

            if (ReferenceEquals(newTile, null))
            {
                continue;
            }
            validNeighbours.Add(newTile);
        }
        for (int z = -1; z <= 1; z++)
        {
            if (z == 0)
            {
                continue;
            }
            Vector3Int tilePos = givenTile.GetPos + new Vector3Int(0, 0, z);
            TileData newTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tilePos);

            if (ReferenceEquals(newTile, null))
            {
                continue;
            }
            validNeighbours.Add(newTile);
        }

        return validNeighbours;
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void SetPlayerStartTile()
    {
        startTile = GetRandomTile();
    }
    public void PlaceEnemies()
    {
        foreach (var item in enemies)
        {
            TileData startingTile = GetRandomTile();
            item.Movement.SetEnemyStartPosition(startingTile);
            item.OnEnteredLevel?.Invoke(this, item);
        }
    }
    private TileData GetRandomTile()
    {
        /*  TileData tile = traversableGround[Random.Range(0, traversableGround.Count)];
          return tile;*/
        TileData tile = null;
        while (true)
        {
            if (!ReferenceEquals(tile, null) && !tile.Occupied)
            {
                return tile;
            }
            tile = traversableGround[UnityEngine.Random.Range(0, traversableGround.Count)];
        }
    }
    public IEnumerator PlacePlayerAtStart()
    {
        GameManager.Instance.PlayerWrapper.PlayerMovement.SetCurrentTile(startTile);
        yield return new WaitForEndOfFrame();
        GameManager.Instance.PlayerWrapper.OnEnteredLevel?.Invoke(this, GameManager.Instance.PlayerWrapper);
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

    private void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(GameManager.Instance.enemyPrefab, transform);
        newEnemy.SetUpEnemy(EnemyCreator.GetEnemyAnimalFromValue(UnityEngine.Random.Range(0f, 1f)));
        Debug.Log("spawned " + newEnemy.RefAnimal);
        enemies.Add(newEnemy);
    }

    public bool CheckStraightLineX(TileData start, TileData dest)
    {
        if (start.GetPos.x != dest.GetPos.x)
        {
            return false;
        }
        int mod = 1;
        if (start.GetPos.x >= dest.GetPos.x)
        {
            mod = -1;
        }
        int distance = Mathf.Abs(dest.GetPos.x) - Mathf.Abs(start.GetPos.x);
        for (int i = start.GetPos.x + mod; i < distance; i += mod)
        {
            if (ReferenceEquals(GetTile(new Vector3Int(i, 0, start.GetPos.z)), null))
            {
                return false;
            }
        }
        return true;

    }
    public bool CheckStraightLineZ(TileData start, TileData dest)
    {
        if (start.GetPos.z != dest.GetPos.z)
        {
            return false;
        }
        int mod = 1;
        if (start.GetPos.z >= dest.GetPos.z)
        {
            mod = -1;
        }
        int distance = Mathf.Abs(dest.GetPos.z) - Mathf.Abs(start.GetPos.z);
        for (int i = start.GetPos.z + mod; i < distance; i += mod)
        {
            if (ReferenceEquals(GetTile(new Vector3Int(start.GetPos.x, 0, i)), null))
            {
                return false;
            }
        }
        return true;
    }
}

[System.Serializable]
public class TileData : IHeapItem<TileData>
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private InteractableTile overlay;
    [SerializeField] private Vector3Int Pos;
    [SerializeField] private bool occupied = false;
    [SerializeField] private Character subscribedCharacter;
    private int heapIndex;
    public int costToEnd;
    public int costToStart;
    public TileData PathParent;
    public int totalCost => costToEnd + costToStart;
    public GameObject GetObj { get => Obj; }
    public Vector3Int GetPos { get => Pos; }
    public Vector3 GetStandingPos { get => new Vector3(Obj.transform.position.x, Obj.transform.position.y + 0.6f, Obj.transform.position.z); }
    public InteractableTile Overly { get => overlay; }
    public bool Occupied { get => occupied; set => occupied = value; }
    public int HeapIndex { get => heapIndex; set => heapIndex = value; }

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

    public void SubscribeCharacter(Character givenCharacter)
    {
        subscribedCharacter = givenCharacter;
        occupied = true;
    }
    public void UnSubscribeCharacter()
    {
        subscribedCharacter = null;
        occupied = false;
    }

    public void HitTile(AnimalAttack givenAttack, DamageDealer Dealer = null)
    {
        subscribedCharacter?.Damageable.GetHit(givenAttack, Dealer);
    }

    public int CompareTo(TileData other)
    {
        int compare = totalCost.CompareTo(other.totalCost);
        if (compare == 0)
        {
            compare = costToEnd.CompareTo(other.costToEnd);
        }
        return -compare;
    }
}
