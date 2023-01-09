using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel;

    public Level CurrentLevel { get => currentLevel; }
}
