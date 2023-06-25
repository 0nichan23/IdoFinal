using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerWrapper playerWrapper;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Pathfinder pathfinder;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private PopupSpawner popupSpawner;
    [SerializeField] private ObjetPoolManager poolManager;
    [SerializeField] private AnimalDropHandler animalDropHandler;
    [SerializeField] private SavingManager savingManager;


    [Header("Asset Dump")]
    public InteractableTile InteractableTilePrefab;
    public Enemy enemyPrefab;

    private void Start()
    {
        Application.targetFrameRate = 300;
    }

    public void StartGame()
    {
        LevelManager.StartGame();
        playerWrapper.StartGame();
    }

    public void ReturnToBase()
    {
        playerWrapper.Stun();
        playerWrapper.Team.ClearPlayerTeam();
        LevelManager.ClearLevels();
        PlayerWrapper.PlayerHud.BackToBaseReset();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
    public InputManager InputManager { get => inputManager; }
    public PlayerWrapper PlayerWrapper { get => playerWrapper; }
    public LevelManager LevelManager { get => levelManager; }
    public Pathfinder Pathfinder { get => pathfinder; }
    public LevelGenerator LevelGenerator { get => levelGenerator; }
    public PopupSpawner PopupSpawner { get => popupSpawner; }
    public ObjetPoolManager PoolManager { get => poolManager; }
    public AnimalDropHandler AnimalDropHandler { get => animalDropHandler; }
    public SavingManager SavingManager { get => savingManager; }
}
