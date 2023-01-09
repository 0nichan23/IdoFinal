using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrapper : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public PlayerMovement PlayerMovement { get => playerMovement;}
}
