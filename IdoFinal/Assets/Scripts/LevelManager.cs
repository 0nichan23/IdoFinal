using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel;
    [SerializeField] private Queue<Level> levels = new Queue<Level>();
    [SerializeField] private int amount;
    [SerializeField] private int enemyAmount;
    public UnityEvent<Level> OnMoveToNextLevel;

    public Level CurrentLevel { get => currentLevel; }
    public Queue<Level> Levels { get => levels; }

    public void StartGame()
    {
        CreateLevels();
        StartRun();
    }
    private void OnValidate()
    {
        if (amount < 1)
        {
            amount = 1;
        }
    }
    private void CreateLevels()
    {
        for (int i = 0; i < amount; i++)
        {
            Level newLevel = GameManager.Instance.LevelGenerator.CreateLevel();
            newLevel.SetUpLevel(enemyAmount);
            levels.Enqueue(newLevel);
            newLevel.gameObject.SetActive(false);
        }
    }
    private void StartRun()
    {
        currentLevel = levels.Dequeue();
        currentLevel.gameObject.SetActive(true);
        StartCoroutine(currentLevel.PlacePlayerAtStart());
        currentLevel.PlaceEnemies();
    }

    [ContextMenu("Move To Next")]
    public void MoveToNextLevel()
    {
        currentLevel.gameObject.SetActive(false);
        GameManager.Instance.PlayerWrapper.OnExitLevel?.Invoke(CurrentLevel, GameManager.Instance.PlayerWrapper);
        ExitEnemies();
        currentLevel = levels.Dequeue();
        currentLevel.gameObject.SetActive(true);
        StartCoroutine(currentLevel.PlacePlayerAtStart());
        currentLevel.PlaceEnemies();
        OnMoveToNextLevel?.Invoke(CurrentLevel);
    }

    public void DealDamageOnTiles(List<Vector3Int> givenPositions, AnimalAttack givenAttack, Character dealer)
    {
        foreach (var item in givenPositions)
        {
            TileData tile = currentLevel.GetTile(item, dealer.CurrentTileMap);
            if (ReferenceEquals(tile, null))
            {
                continue;
            }

            tile.HitTile(givenAttack, dealer.DamageDealer);

            tile.Overly.gameObject.SetActive(true);
            tile.Overly.DamageColor();
        }
    }

    private void ExitEnemies()
    {//idk why i did this
        foreach (var item in currentLevel.Enemies)
        {
            item.OnExitLevel?.Invoke(CurrentLevel, item);
        }
    }

    public void ClearLevels()
    {
        currentLevel.gameObject.SetActive(false);
        Destroy(CurrentLevel.gameObject);
        int times = levels.Count;
        for (int i = 0; i < times; i++)
        {
            Destroy(levels.Dequeue().gameObject);
        }
        levels.Clear();
    }

}
