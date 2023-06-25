using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private FileData fileData;
    private GameData gameData;
    private List<ISaveable> saveables;

    public GameData GameData { get => gameData; }

    private void Start()
    {
        fileData = new FileData(Application.persistentDataPath, fileName);
        saveables = GetAllSaveables();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileData.Load();
        if (ReferenceEquals(gameData, null))
        {
            NewGame();
            Debug.Log("no saved data found");
        }
        foreach (var item in saveables)
        {
            item.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var item in saveables)
        {
            item.SaveGame(ref gameData);
        }
        fileData.Save(gameData);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveable> GetAllSaveables()
    {
        IEnumerable<ISaveable> saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        return new List<ISaveable>(saveables);
    }
}
