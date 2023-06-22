using System.Collections.Generic;
using UnityEngine;

public class DropsInventory : MonoBehaviour
{
    [SerializeField] private List<BasicDropSO> items = new List<BasicDropSO>();
    public List<BasicDropSO> Items { get => items; }

    public Dictionary<BasicDropSO, int> GetDropsByAmount()
    {
        Dictionary<BasicDropSO, int> itemByAmount = new Dictionary<BasicDropSO, int>();
        foreach (var item in items)
        {
            if (itemByAmount.ContainsKey(item))
            {
                itemByAmount[item] += 1;
            }
            else
            {
                itemByAmount.Add(item, 1);
            }
        }
        return itemByAmount;
    }

    public void AddDrop(BasicDropSO drop)
    {
        items.Add(drop);
    }


    public void RemoveDrop(BasicDropSO drop)
    {
        items.Remove(drop);
    }
}
