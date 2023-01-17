using UnityEngine;
using UnityEngine.Events;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private Animal activeAnimal;
    [SerializeField] private Animal[] backLineAnimals = new Animal[4];

    public UnityEvent OnSwitchActiveAnimal;
    public Animal ActiveAnimal { get => activeAnimal; }
    public Animal[] BackLineAnimals { get => backLineAnimals; }
}


