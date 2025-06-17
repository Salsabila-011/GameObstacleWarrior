using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [SerializeField] float groundeCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 8f;

    bool isGrounded;
    bool hasControl = true;

    float ySpeed;

    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

    private void Awake()
    {
        
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        var moveInput = new Vector3(h, 0, v).normalized;
        var moveDir = cameraController.PlanarRotation * moveInput;

        if (!hasControl)
            return;

        GroundCheck();

        // Tambahkan logika lompatan
        if (isGrounded)
        {
            ySpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
                
            }
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (moveAmount > 0)
            targetRotation = Quaternion.LookRotation(moveDir);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

    }

    void GroundCheck()
    {
       isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundeCheckRadius, groundLayer);
    }

    public void SetControl(bool hasControl)
    {
        this.hasControl = hasControl;
        characterController.enabled = hasControl;

        if (!hasControl)
        {
            animator.SetFloat("moveAmount", 0f);
            targetRotation = transform.rotation;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundeCheckRadius);
    }
}
