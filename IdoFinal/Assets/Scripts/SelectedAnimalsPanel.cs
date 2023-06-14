using System.Collections.Generic;
using UnityEngine;


public class SelectedAnimalsPanel : MonoBehaviour
{
    [SerializeField] private List<AnimalInventorySlot> selectedSlots = new List<AnimalInventorySlot>();

    public List<AnimalInventorySlot> SelectedSlots { get => selectedSlots; }

    private void Start()
    {
        SubscribeSlots();
    }

    private void SubscribeSlots()
    {
        foreach (var item in selectedSlots)
        {
            item.Button.onClick.AddListener(item.UnselectSlot);
        }
    }

    public void SelectSlot(AnimalInventorySlot givenSlot)
    {
        foreach (var item in selectedSlots)
        {
            if (ReferenceEquals(item.RefAnimal, null))//if the slot is empty
            {
                item.SetUp(givenSlot.RefAnimal);
                return;
            }
        }
    }

}
