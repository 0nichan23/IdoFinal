using UnityEngine;

public class PlayerWrapper : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimationHandler playerAnimationHandler;
    [SerializeField] private Transform gfx;

    public PlayerMovement PlayerMovement { get => playerMovement; }
    public PlayerAnimationHandler PlayerAnimationHandler { get => playerAnimationHandler; }
    public Transform Gfx { get => gfx; set => gfx = value; }
}
