using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageHandler
{
    [SerializeField] private float baseAmount;
    private List<float> mods = new List<float>();
    public List<float> Mods { get => mods; }
    public float BaseAmount { get => baseAmount; set => baseAmount = value; }

    public void AddMod(float mod)
    {
        mods.Add(mod);
    }

    public void ClearMods()
    {
        mods.Clear();
    }

    public float CalcFinalDamageMult()
    {
        float amount = baseAmount;
        foreach (var item in mods)
        {
            if (item >= 1)
            {
                amount += (item * BaseAmount) - BaseAmount;//add damage
            }
            else
            {
                amount -= BaseAmount - (item * BaseAmount);//reduce damage
            }
        }
        return amount;
    }


}
