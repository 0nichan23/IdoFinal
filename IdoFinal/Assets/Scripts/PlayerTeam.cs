using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTeam : MonoBehaviour
{
    [SerializeField] private AnimalModelData activeAnimal;
    [SerializeField] private List<AnimalModelData> backLineAnimals = new List<AnimalModelData>();
    [SerializeField] private List<Animal> startingTeam = new List<Animal>();
    private List<AnimalModelData> createdAnimalData = new List<AnimalModelData>();
    public UnityEvent OnSwitchActiveAnimal;
    public AnimalModelData ActiveAnimal { get => activeAnimal; }
    public List<AnimalModelData> BackLineAnimals { get => backLineAnimals; }

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
        if (ReferenceEquals(activeAnimal, null))
        {
            return;
        }
        activeAnimal.Animal.Passive.UnSubscribePassive(GameManager.Instance.PlayerWrapper);
        foreach (var item in BackLineAnimals)
        {
            item.Animal.Passive.UnSubscribePassive(GameManager.Instance.PlayerWrapper);
        }
    }

    public void CacheStartTeam()
    {
        SetNewTeam(startingTeam);
        GameManager.Instance.PlayerWrapper.SetAnimalStatsOnComps();
    }

    public GameObject CreateModel(Animal animal, bool state = false)
    {
        GameObject model = Instantiate(animal.AnimalModel, GameManager.Instance.PlayerWrapper.Gfx.transform);
        model.transform.localPosition = Vector3.zero;
        GameManager.Instance.PlayerWrapper.PlayerAnimationHandler.AddAnims(model.transform);
        model.SetActive(state);
        return model;
    }

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

    public AnimalModelData GetAnimalModelDataFromAnimal(Animal animal, bool state = false)
    {
        foreach (var modelData in createdAnimalData)
        {
            if (ReferenceEquals(modelData.Animal, animal))
            {
                return modelData;//in case the animal was already used before
            }
        }
        AnimalModelData amd = new AnimalModelData(animal, CreateModel(animal, state));//first time using the animal
        createdAnimalData.Add(amd);

        return amd;
    }

    public void SetNewTeam(List<Animal> givenTeam)
    {
        activeAnimal = null;
        UnSubscribeTeamPassives();
        backLineAnimals.Clear();
        foreach (var item in createdAnimalData)
        {
            item.Model.SetActive(false);
        }
        for (int i = 0; i < givenTeam.Count; i++)
        {
            AnimalModelData md;
            if (i == 0)
            {
                md = GetAnimalModelDataFromAnimal(givenTeam[i], true);
                activeAnimal = md;
                OnSwitchActiveAnimal?.Invoke();
            }
            else
            {
                md = GetAnimalModelDataFromAnimal(givenTeam[i]);
                backLineAnimals.Add(md);
            }
        }
        SubscirbeTeamPassives();
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


