using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private List<TileData> traversableGround = new List<TileData>();
    [SerializeField] private List<TileData> swimmingMap = new List<TileData>();
    [SerializeField] private List<TileData> flyingMap = new List<TileData>();
    private List<TileData> totalMap = new List<TileData>();
    private List<TileData> obstacleMap = new List<TileData>();
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
    public List<TileData> SwimmingMap { get => swimmingMap; }
    public List<TileData> FlyingMap { get => flyingMap; }
    public List<TileData> TotalMap { get => totalMap; }
    public List<TileData> ObstacleMap { get => obstacleMap; }

    public void SetUpLevel(int enemyAmount)
    {
        SetTraversableGround();
        SpawnEnemies(enemyAmount);
    }

    private void SetTraversableGround()
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            Transform child = tilemap.transform.GetChild(i);
            Vector3Int tilePos = new Vector3Int(Mathf.FloorToInt(child.localPosition.x), 0, Mathf.FloorToInt(child.localPosition.z));
            TileData newTile = new TileData(tilePos, child.gameObject);
            if (child.gameObject.CompareTag("Walkable"))
            {
                traversableGround.Add(newTile);
                flyingMap.Add(newTile);
                totalMap.Add(newTile);
            }
            if (child.gameObject.CompareTag("Swimmable"))
            {
                swimmingMap.Add(newTile);
                flyingMap.Add(newTile);
                totalMap.Add(newTile);
            }
            if (child.gameObject.CompareTag("Flyable"))
            {
                flyingMap.Add(newTile);
                totalMap.Add(newTile);
            }
            if (child.gameObject.CompareTag("Tree"))
            {
                obstacleMap.Add(newTile);
            }
        }

        foreach (var tile in flyingMap)
        {
            InteractableTile interTile = Instantiate(GameManager.Instance.InteractableTilePrefab, tile.GetObj.transform);
            interTile.transform.position = new Vector3(tile.GetStandingPos(MovementMode.Ground).x, tile.GetStandingPos(MovementMode.Ground).y + 0.2f, tile.GetStandingPos(MovementMode.Ground).z);
            tile.CacheOverlayObject(interTile);
            interTile.gameObject.SetActive(false);
        }
        OnDoneCreatingRoom?.Invoke();
    }

    public List<TileData> GetNeighbours(TileData givenTile, List<TileData> givenTileMap)
    {
        List<TileData> validNeighbours = new List<TileData>();
        for (int x = -1; x <= 1; x++)
        {
            if (x == 0)
            {
                continue;
            }
            Vector3Int tilePos = givenTile.GetPos + new Vector3Int(x, 0, 0);
            TileData newTile = GetTile(tilePos, givenTileMap);

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
            TileData newTile = GameManager.Instance.LevelManager.CurrentLevel.GetTile(tilePos, givenTileMap);

            if (ReferenceEquals(newTile, null))
            {
                continue;
            }
            validNeighbours.Add(newTile);
        }

        return validNeighbours;
    }

    public void SetPlayerStartTile()
    {
        startTile = GetRandomTile(traversableGround);
    }
    public void PlaceEnemies()
    {
        foreach (var item in enemies)
        {
            item.SetStartTraversal();
            item.gameObject.SetActive(true);
            TileData startingTile = GetRandomTile(item.CurrentTileMap);
            item.Movement.SetEnemyStartPosition(startingTile);
            item.OnEnteredLevel?.Invoke(this, item);
        }
    }
    private TileData GetRandomTile(List<TileData> givenTileMap)
    {
        TileData tile = null;
        while (true)
        {
            if (!ReferenceEquals(tile, null) && !tile.Occupied)
            {
                return tile;
            }
            tile = givenTileMap[Random.Range(0, givenTileMap.Count)];
        }
    }
    public IEnumerator PlacePlayerAtStart()
    {
        GameManager.Instance.PlayerWrapper.SetStartTraversal();
        GameManager.Instance.PlayerWrapper.PlayerMovement.SetCurrentTile(GetRandomTile(GameManager.Instance.PlayerWrapper.CurrentTileMap));
        yield return new WaitForEndOfFrame();
        GameManager.Instance.PlayerWrapper.OnEnteredLevel?.Invoke(this, GameManager.Instance.PlayerWrapper);
    }

    public TileData GetTile(Vector3Int givenPos, List<TileData> givenMap)
    {
        if (ReferenceEquals(givenMap, null))
        {
            return null;
        }
        foreach (var item in givenMap)
        {
            if (item.GetPos == givenPos)
            {
                return item;
            }
        }
        return null;
    }

    private void SpawnEnemies(int enemyAmount)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Enemy newEnemy = Instantiate(GameManager.Instance.enemyPrefab, transform);
            newEnemy.gameObject.SetActive(false);
            newEnemy.SetUpEnemy(EnemyCreator.GetEnemyAnimalFromValue(Random.Range(0f, 1f)));
            enemies.Add(newEnemy);
        }
    }

    public List<TileData> GetMapFromMovementMode(MovementMode mm)
    {
        switch (mm)
        {
            case MovementMode.Ground:
                return TraversableGround;
            case MovementMode.Water:
                return swimmingMap;
            case MovementMode.Air:
                return flyingMap;
        }
        return null;
    }

    public bool CheckStraightLineX(TileData start, TileData dest, List<TileData> givenMap)
    {
        if (start.GetPos.z != dest.GetPos.z)
        {
            return false;
        }
        int mod = 1;
        if (start.GetPos.x >= dest.GetPos.x)
        {
            mod = -1;
        }
        int distance = GameManager.Instance.Pathfinder.GetDistanceOfTiles(start.GetPos, dest.GetPos);
        for (int i =0 + mod; i < distance; i ++)
        {
            if (ReferenceEquals(GetTile(new Vector3Int(start.GetPos.x + (i * mod), 0, start.GetPos.z), givenMap), null))
            {
                return false;
            }
        }
        return true;

    }
    public bool CheckStraightLineZ(TileData start, TileData dest, List<TileData> givenMap)
    {
        if (start.GetPos.x != dest.GetPos.x)
        {
            return false;
        }
        int mod = 1;
        if (start.GetPos.z >= dest.GetPos.z)
        {
            mod = -1;
        }
        int distance = GameManager.Instance.Pathfinder.GetDistanceOfTiles(start.GetPos, dest.GetPos);
        for (int i = 0 + mod; i < distance; i++)
        {
            if (ReferenceEquals(GetTile(new Vector3Int(start.GetPos.x, 0, start.GetPos.z + (i * mod)), givenMap), null))
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
    public Vector3 GetGroundPos { get => new Vector3(Obj.transform.position.x, Obj.transform.position.y + 0.6f, Obj.transform.position.z); }
    public Vector3 GetAirPos { get => new Vector3(Obj.transform.position.x, Obj.transform.position.y + 1.6f, Obj.transform.position.z); }
    public Vector3 GetWaterPos { get => new Vector3(Obj.transform.position.x, Obj.transform.position.y + 0.3f, Obj.transform.position.z); }
    public InteractableTile Overly { get => overlay; }
    public bool Occupied { get => occupied; set => occupied = value; }
    public int HeapIndex { get => heapIndex; set => heapIndex = value; }

    public TileData(Vector3Int pos, GameObject obj)
    {
        Obj = obj;
        Pos = pos;
    }


    public Vector3 GetStandingPos(MovementMode movement)
    {
        switch (movement)
        {
            case MovementMode.Ground:
                return GetGroundPos;
            case MovementMode.Water:
                return GetWaterPos;
            case MovementMode.Air:
                return GetAirPos;
            default:
                return Vector3.zero;
        }
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
