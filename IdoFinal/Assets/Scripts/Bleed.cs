using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffect
{
    private float counter;
    public Bleed(float duration, float amount)
    {

    }
    protected override void Subscribe()
    {
        base.Subscribe();
    }

    public override void Reset()
    {
        counter = 0f;
    }


}
