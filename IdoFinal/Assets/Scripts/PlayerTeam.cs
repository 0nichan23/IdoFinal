using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private AnimalModelData activeAnimal;
    [SerializeField] private AnimalModelData[] backLineAnimals = new AnimalModelData[4];
    private List<AnimalModelData> caughtAnimalData = new List<AnimalModelData>();
    public UnityEvent OnSwitchActiveAnimal;
    public UnityEvent OnAnimalCaught;
    public AnimalModelData ActiveAnimal { get => activeAnimal; }
    public AnimalModelData[] BackLineAnimals { get => backLineAnimals; }

    private void Start()
    {
        CacheStartTeam();
        SubscirbeTeamPassives();
    }

    private void SubscirbeTeamPassives()
    {
        activeAnimal.Animal.Passive.SubscribePassive(GameManager.Instance.PlayerWrapper);
        foreach (var item in BackLineAnimals)
        {
            item.Animal.Passive.SubscribePassive(GameManager.Instance.PlayerWrapper);
        }
    }

    public void UnSubscribeTeamPassives()
    {
        activeAnimal.Animal.Passive.UnSubscribePassive(GameManager.Instance.PlayerWrapper);
        foreach (var item in BackLineAnimals)
        {
            item.Animal.Passive.UnSubscribePassive(GameManager.Instance.PlayerWrapper);
        }
    }

    private void CacheStartTeam()
    {
        CacheAnimal(ActiveAnimal, true);
        foreach (var item in BackLineAnimals)
        {
            CacheAnimal(item);
        }
    }

    public bool CheckAnimalCaught(Animal givenAnimal)
    {
        foreach (var item in caughtAnimalData)
        {
            if (item.Animal == givenAnimal)
            {
                return true;
            }
        }
        return false;
    }

    public void CacheAnimal(AnimalModelData givenAniamlData, bool state = false)
    {
        GameObject model = Instantiate(givenAniamlData.Animal.AnimalModel, GameManager.Instance.PlayerWrapper.Gfx.transform);
        model.transform.localPosition = Vector3.zero;
        givenAniamlData.Model = model;
        caughtAnimalData.Add(givenAniamlData);
        GameManager.Instance.PlayerWrapper.PlayerAnimationHandler.AddAnims(model.transform);
        model.SetActive(state);
    }

  /*  public void TryCatchAnimal(Damageable target)
    {
        if (!CheckAnimalCaught(target.RefAnimal))
        {
            CacheAnimal(target.RefAnimal);
            OnAnimalCaught?.Invoke();
        }
    }*/

    public void SwitchActiveAnimal(int index)
    {
        activeAnimal.Model.gameObject.SetActive(false);
        foreach (var item in BackLineAnimals)
        {
            item.Model.gameObject.SetActive(false);
        }
        AnimalModelData temp = BackLineAnimals[index];
        BackLineAnimals[index] = activeAnimal;
        activeAnimal = temp;
        activeAnimal.Model.gameObject.SetActive(true);
        OnSwitchActiveAnimal?.Invoke();
    }

}

[System.Serializable]
public class AnimalModelData
{
    public Animal Animal;
    public GameObject Model;

    public AnimalModelData(Animal animal, GameObject model)
    {
        Animal = animal;
        Model = model;
    }
}


