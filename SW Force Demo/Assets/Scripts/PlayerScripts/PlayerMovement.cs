using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;

    [SerializeField] private float speed = 5f;
    public float Speed { get => speed; set => speed = value; }

    private float gravity = 20f;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float superJumpForce = 30f;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown(ActionsTags.RESTART))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void MovePlayer()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed * Time.deltaTime;

        ApplyGravity();

        characterController.Move(moveDirection);
    }

    void ApplyGravity()
    {
        //The gravity is always being applied, even touching the ground.
        verticalVelocity -= gravity * Time.deltaTime;

        PlayerJump();

        //Here we multiply Time.deltaTime again because smooth requirements.
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetButtonDown(ActionsTags.JUMP))
        {
            verticalVelocity = jumpForce;
        } else if (characterController.isGrounded && Input.GetButtonDown(ActionsTags.SUPER_JUMP))
        {
            verticalVelocity = superJumpForce;
        }
    }
}
