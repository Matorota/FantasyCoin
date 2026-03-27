using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float gravityValue = -20f;
    [SerializeField] float speed = 4f;
    [Tooltip("The exact height the player will jump in Unity units (e.g., 1.3 blocks)")]
    [SerializeField] float jumpHeight = 1.3f;
    [SerializeField] float smoothTime = 0.15f;

    private Vector3 playerVelocity;
    private Vector3 currentMove;
    private Vector3 moveVelocity;

    private readonly Vector3 isoForward = new Vector3(1, 0, 1).normalized;
    private readonly Vector3 isoRight = new Vector3(1, 0, -1).normalized;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool grounded = controller.isGrounded;

        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard.aKey.isPressed) horizontal = -1f;
        if (keyboard.dKey.isPressed) horizontal = 1f;
        if (keyboard.wKey.isPressed) vertical = 1f;
        if (keyboard.sKey.isPressed) vertical = -1f;

        Vector3 targetMove = (isoForward * vertical + isoRight * horizontal);
        if (targetMove.magnitude > 1f) targetMove.Normalize();
        targetMove *= speed;

        currentMove = Vector3.SmoothDamp(currentMove, targetMove, ref moveVelocity, smoothTime);


        Vector3 finalMovement = currentMove;

        if (currentMove.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }

        if (keyboard.spaceKey.wasPressedThisFrame && grounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        finalMovement.y = playerVelocity.y;

        controller.Move(finalMovement * Time.deltaTime);
    }

    public void ResetVelocity()
    {
        playerVelocity = Vector3.zero;
        currentMove = Vector3.zero;
        moveVelocity = Vector3.zero;
    }
}