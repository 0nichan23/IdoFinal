using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel;
    [SerializeField] private Queue<Level> levels = new Queue<Level>();
    [SerializeField] private int Amount;

    public Level CurrentLevel { get => currentLevel; }
    public Queue<Level> Levels { get => levels; }

    private void Start()
    {
        CreateLevels();
        StartRun();
    }
    private void OnValidate()
    {
        if (Amount < 1)
        {
            Amount = 1;
        }
    }
    private void CreateLevels()
    {
        for (int i = 0; i < Amount; i++)
        {
            Level newLevel = GameManager.Instance.LevelGenerator.CreateLevel();
            newLevel.SetUpLevel();
            levels.Enqueue(newLevel);
            newLevel.gameObject.SetActive(false);
        }
    }
    private void StartRun()
    {
        currentLevel = levels.Dequeue();
        currentLevel.gameObject.SetActive(true);
        currentLevel.SetPlayerStartTile();
        currentLevel.PlacePlayerAtStart();
        currentLevel.PlaceEnemies();
    }

    [ContextMenu("Move To Next")]
    public void MoveToNextLevel()
    {
        currentLevel.gameObject.SetActive(false);
        currentLevel = levels.Dequeue();
        currentLevel.gameObject.SetActive(true);
        currentLevel.SetPlayerStartTile();
        currentLevel.PlacePlayerAtStart();
        currentLevel.PlaceEnemies();
    }

    public void DealDamageOnTiles(List<Vector3Int> givenPositions, AnimalAttack givenAttack, DamageDealer dealer = null)
    {
        foreach (var item in givenPositions)
        {
            TileData tile = currentLevel.GetTile(item);
            if (ReferenceEquals(tile, null))
            {
                continue;
            }

            tile.HitTile(givenAttack, dealer);

            tile.Overly.gameObject.SetActive(true);
            tile.Overly.DamageColor();
        }
    }



}
