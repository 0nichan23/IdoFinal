using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour
{
    [SerializeField] private ActiveEffectsBar bar;
    [SerializeField] private Bar healthBar;
    
    public ActiveEffectsBar Bar { get => bar; }
    public Bar HealthBar { get => healthBar; }
}
