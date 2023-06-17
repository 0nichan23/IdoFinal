using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    private List<Animator> controllers = new List<Animator>();
    private Character owner;
    public void CacheOwner(Character givenCharacter)
    {
        owner = givenCharacter;
    }
    public void StartSwimAnim()
    {
        foreach (var anim in controllers)
        {
            anim.SetBool("Swim", true);
        }
    }
    public void EndSwimAnim()
    {
        foreach (var anim in controllers)
        {
            anim.SetBool("Swim", false);
        }
    }
    public void StartWalkAnim()
    {
        string Boolean ="";
        switch (owner.MovementMode)
        {
            case MovementMode.Ground:
                Boolean = "Walk";
                break;
            case MovementMode.Water:
                Boolean = "Swim";
                break;
            case MovementMode.Air:
                Boolean = "Fly";
                break;
        }
        foreach (var anim in controllers)
        {
            anim.SetBool(Boolean, true);
        }
    }
    public void EndWalkAnim()
    {
        string Boolean = "";
        switch (owner.MovementMode)
        {
            case MovementMode.Ground:
                Boolean = "Walk";
                break;
            case MovementMode.Water:
                Boolean = "Swim";
                break;
            case MovementMode.Air:
                Boolean = "Fly";
                break;
        }
        foreach (var anim in controllers)
        {
            anim.SetBool(Boolean, false);
        }
    }
    public void AttackAnim()
    {
        foreach (var anim in controllers)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void DeathAnimation()
    {
        foreach (var anim in controllers)
        {
            anim.SetTrigger("Die");
        }
    }

    public void AddAnims(Transform model)
    {
        Animator[] anims = model.GetComponentsInChildren<Animator>();
        foreach (var anim in anims)
        {
            controllers.Add(anim);
        }
    }
}
