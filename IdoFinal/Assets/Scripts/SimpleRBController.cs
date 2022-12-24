using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleRBController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] LayerSensor groundCheck;
    [SerializeField] private float speed;
    public Rigidbody Rb { get => rb; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InputManager.Instance.OnJumpDown.AddListener(Jump);
    }

    private void Update()
    {
        rb.velocity = new Vector3(InputManager.Instance.GetMoveVector().x * speed, rb.velocity.y, 0);
    }

    public void Jump()
    {
        if (groundCheck.IsTouching())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }


}
