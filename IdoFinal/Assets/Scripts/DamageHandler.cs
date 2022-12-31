using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageHandler
{
    [SerializeField] private float baseAmount;
    private List<float> mods = new List<float>();
    public List<float> Mods { get => mods; }

    public void AddMod(float mod)
    {
        mods.Add(mod);
    }

    public void ClearMods()
    {
        mods.Clear();
    }

    public float CalcFinalDamage()
    {
        float amount = baseAmount;
        foreach (var item in mods)
        {
            amount *= item;
        }
        return amount;
    }
}
