using UnityEngine;
using UnityEngine.Events;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private Animal activeAnimal;
    [SerializeField] private Animal[] backLineAnimals = new Animal[4];

    public UnityEvent OnSwitchActiveAnimal;
    public Animal ActiveAnimal { get => activeAnimal; }
    public Animal[] BackLineAnimals { get => backLineAnimals; }

    private void Start()
    {
        SubscirbeTeamPassives();
    }

    private void SubscirbeTeamPassives()
    {
        activeAnimal.Passive.SubscribePassive(GameManager.Instance.PlayerWrapper);
        foreach (var item in BackLineAnimals)
        {
            item.Passive.SubscribePassive(GameManager.Instance.PlayerWrapper);
        }
    }
}


