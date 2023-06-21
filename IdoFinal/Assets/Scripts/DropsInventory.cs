using System.Collections.Generic;
using UnityEngine;

public class DropsInventory : MonoBehaviour
{
    [SerializeField] private List<BasicDropSO> items = new List<BasicDropSO>();
    public List<BasicDropSO> Items { get => items; }

    public void AddDrop(BasicDropSO drop)
    {
        items.Add(drop);
    }

}
