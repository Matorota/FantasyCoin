using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerTileMovement : MonoBehaviour
{
    [Header("Grid Settings")]
    [Tooltip("How far the player moves per button press (usually 1 for 1x1 blocks)")]
    [SerializeField] private float tileSize = 1f;
    [Tooltip("How fast the player slides to the next tile")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump & Physics")]
    [SerializeField] private float jumpHeight = 1.3f;
    [SerializeField] private float gravityValue = -20f;

    private CharacterController controller;
    private Vector3 targetPosXZ;
    private float verticalVelocity;

    private bool isStrictlyGrounded = true;

    private readonly Vector3 isoUpRight = new Vector3(1, 0, 0);
    private readonly Vector3 isoDownLeft = new Vector3(-1, 0, 0);
    private readonly Vector3 isoUpLeft = new Vector3(0, 0, 1);
    private readonly Vector3 isoDownRight = new Vector3(0, 0, -1);

    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void Update()
    {
        HandleJumpAndGravity();
        HandleTileMovement();
    }

    private void HandleJumpAndGravity()
    {
        isStrictlyGrounded = Physics.SphereCast(transform.position + Vector3.up * 0.1f, 0.15f, Vector3.down, out _, 0.35f, ~0, QueryTriggerInteraction.Ignore);

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame && isStrictlyGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        verticalVelocity += gravityValue * Time.deltaTime;
    }

    private void HandleTileMovement()
    {
        Vector3 currentPosXZ = new Vector3(transform.position.x, 0, transform.position.z);

        if (Vector3.Distance(currentPosXZ, targetPosXZ) > tileSize * 2f)
        {
            targetPosXZ = currentPosXZ;
        }

        bool isAtTileCenter = Vector3.Distance(currentPosXZ, targetPosXZ) < 0.05f;

        if (isAtTileCenter)
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                Vector3 moveDirection = Vector3.zero;

                if (keyboard.wKey.isPressed) moveDirection = isoUpLeft;
                else if (keyboard.sKey.isPressed) moveDirection = isoDownRight;
                else if (keyboard.dKey.isPressed) moveDirection = isoUpRight;
                else if (keyboard.aKey.isPressed) moveDirection = isoDownLeft;

                if (moveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(moveDirection);

                    float radius = controller.radius - 0.05f;

                    Vector3 p1 = transform.position + controller.center + Vector3.up * (controller.height / 2f - radius);

                    Vector3 p2 = transform.position + controller.center - Vector3.up * (controller.height / 2f - radius - controller.stepOffset);

                    if (!Physics.CapsuleCast(p1, p2, radius, moveDirection, out _, tileSize, ~0, QueryTriggerInteraction.Ignore))
                    {
                        targetPosXZ += (moveDirection * tileSize);
                    }
                }
            }
        }

        Vector3 expectedXZMovement = Vector3.zero;
        if (Vector3.Distance(currentPosXZ, targetPosXZ) > 0.001f)
        {
            Vector3 towardsTarget = Vector3.MoveTowards(currentPosXZ, targetPosXZ, moveSpeed * Time.deltaTime);
            expectedXZMovement = towardsTarget - currentPosXZ;
        }

        Vector3 finalMovement = expectedXZMovement;
        finalMovement.y = verticalVelocity * Time.deltaTime;

        Vector3 positionBeforeMove = transform.position;

        controller.Move(finalMovement);

        if (!isAtTileCenter)
        {
            Vector3 newPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
            float actualDistanceMovedXZ = Vector3.Distance(new Vector3(positionBeforeMove.x, 0, positionBeforeMove.z), newPosXZ);

            if (expectedXZMovement.magnitude > 0.01f && actualDistanceMovedXZ < 0.005f)
            {
                targetPosXZ = newPosXZ;
            }
        }
    }
}