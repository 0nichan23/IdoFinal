using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    private List<Animator> controllers = new List<Animator>();
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
        foreach (var anim in controllers)
        {
            anim.SetBool("Walk", true);
        }
    }
    public void EndWalkAnim()
    {
        foreach (var anim in controllers)
        {
            anim.SetBool("Walk", false);
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
