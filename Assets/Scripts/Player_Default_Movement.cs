using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float gravityValue = -20f;
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float smoothTime = 0.15f; 

    private Vector3 playerVelocity;
    private Vector3 lastMoveDirection;
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
            playerVelocity.x = 0f;
            playerVelocity.z = 0f;
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

        if (targetMove != Vector3.zero)
        {
            lastMoveDirection = targetMove.normalized;
        }

        currentMove = Vector3.SmoothDamp(currentMove, targetMove, ref moveVelocity, smoothTime);

        if (grounded)
        {
            controller.Move(currentMove * Time.deltaTime);
        }
        else
        {
            controller.Move(new Vector3(playerVelocity.x, 0, playerVelocity.z) * Time.deltaTime);
        }

        if (currentMove.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }

        if (keyboard.spaceKey.wasPressedThisFrame && grounded)
        {
            playerVelocity.y = jumpForce;

            if (lastMoveDirection != Vector3.zero && (horizontal != 0f || vertical != 0f))
            {
                playerVelocity.x = lastMoveDirection.x * speed * 0.3f;
                playerVelocity.z = lastMoveDirection.z * speed * 0.3f;
            }
            else
            {
                playerVelocity.x = 0f;
                playerVelocity.z = 0f;
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}