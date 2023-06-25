using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPanel : MonoBehaviour
{
    [SerializeField] private Image content;
    [SerializeField] private DropIcon iconPrefab;
    [SerializeField] private Image selectedFood;
    [SerializeField] private Image selectedBiomeDrop;
    [SerializeField] private AnimalUnlockPopup unlockPopup;
    private BasicDropSO selectedFoodSO;
    private BasicDropSO selectedBiomeDropSO;
    private AnimalCreator animalCreator =  new AnimalCreator();
    public void OpenFoodPanel()
    {
        ClearContent();
        Dictionary<BasicDropSO, int> itemsByAmount = GameManager.Instance.PlayerWrapper.DropsInventory.GetDropsByAmount();
        foreach (var item in itemsByAmount)
        {
            if (item.Key is FoodDropSO)
            {
                DropIcon newIcon = Instantiate(iconPrefab, content.transform);
                newIcon.SetUp(item.Key ,item.Key.Artwork, item.Value, newIcon.SelectFood);
            }
        }
    }

    public void OpenBiomeDropPanel()
    {
        ClearContent();
        Dictionary<BasicDropSO, int> itemsByAmount = GameManager.Instance.PlayerWrapper.DropsInventory.GetDropsByAmount();
        foreach (var item in itemsByAmount)
        {
            if (item.Key is BiomeDropSO)
            {
                DropIcon newIcon = Instantiate(iconPrefab, content.transform);
                newIcon.SetUp(item.Key, item.Key.Artwork, item.Value, newIcon.SelectBiomeDrop);
            }
        }
    }

    private void ClearContent()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void SelectFoodIcon(DropIcon icon)
    {
        selectedFood.sprite = icon.Image.sprite;
        selectedFoodSO = icon.RefDrop;
        ClearContent();
    }
    public void SelectBiomeDropIcon(DropIcon icon)
    {
        selectedBiomeDrop.sprite = icon.Image.sprite;
        selectedBiomeDropSO = icon.RefDrop;
        ClearContent();
    }

    public void Craft()
    {
        Animal animalToAdd = animalCreator.CraftAnimal(selectedFoodSO as FoodDropSO, selectedBiomeDropSO as BiomeDropSO);
        selectedBiomeDrop.sprite = null;
        selectedFood.sprite = null;
        if (!ReferenceEquals(animalToAdd, null))
        {
            GameManager.Instance.PlayerWrapper.DropsInventory.RemoveDrop(selectedBiomeDropSO);
            GameManager.Instance.PlayerWrapper.DropsInventory.RemoveDrop(selectedFoodSO);
            Debug.Log("added " + animalToAdd.name);
            GameManager.Instance.PlayerWrapper.AnimalInventory.AddAnimal(animalToAdd);
            unlockPopup.Setup(animalToAdd.Portrait);
            unlockPopup.gameObject.SetActive(true);
        }
    }
}
