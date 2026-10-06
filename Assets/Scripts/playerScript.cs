using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("Input Actions")]
    public InputAction moveAction;
    public InputAction jumpAction;

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
            transform.forward = move;

        if (groundedPlayer && jumpAction.WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }
}
