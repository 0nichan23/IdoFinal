using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableAnimalPanel : MonoBehaviour
{
    //need slots that hold animal and image
    [SerializeField] private AnimalInventorySlot slotPrefab;
    [SerializeField] private List<AnimalInventorySlot> slots = new List<AnimalInventorySlot>();
    [SerializeField] private Transform content;


    public void AddSlot(Animal givenAnimal)
    {
        AnimalInventorySlot newSlot = Instantiate(slotPrefab, content);
        newSlot.SetUp(givenAnimal);
        slots.Add(newSlot);
        newSlot.Button.onClick.AddListener(newSlot.SelectSlot);
    }

   

}
