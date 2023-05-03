using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerWrapper playerWrapper;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Pathfinder pathfinder;
    [SerializeField] private LevelGenerator levelGenerator;


    [Header("Asset Dump")]
    public InteractableTile InteractableTilePrefab;

    private void Start()
    {
        Application.targetFrameRate = 300;
    }

    public InputManager InputManager { get => inputManager; }
    public PlayerWrapper PlayerWrapper { get => playerWrapper; }
    public LevelManager LevemManager { get => levelManager; }
    public Pathfinder Pathfinder { get => pathfinder; }
    public LevelGenerator LevelGenerator { get => levelGenerator; }
}
