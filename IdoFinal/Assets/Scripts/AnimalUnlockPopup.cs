using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUnlockPopup : MonoBehaviour
{
    [SerializeField] private Image image;
    
    public void Setup(Sprite givenSprite)
    {
        image.sprite = givenSprite;
    }    
}
