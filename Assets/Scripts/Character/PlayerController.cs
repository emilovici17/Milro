using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float deltaX = -10.0f;
    public float deltaY = 20.0f;

    // Settings related to player movement
    [SerializeField]
    private MovementSettingsObject movementSettings;

    // Used for interacting with colliders (i.e. player hitting crates)
    public float pushForce = 3.0f;

    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    Transform cameraT;
    CharacterController controller;
    Transform chest;
    Transform upperBody;

    bool isEquipped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        upperBody = GetComponentInChildren<ModelBodyParts>().UpperBody;
        chest = GetComponentInChildren<ModelBodyParts>().Chest;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        Move(inputDir, running);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            isEquipped = !isEquipped;
            animator.SetBool("isEquipped", isEquipped);
            animator.SetInteger("weaponType", 1);
            Camera.main.GetComponent<ThirdPersonCamera>().SwitchTarget();
        }

        // animator
        float animationSpeedPercent = ((running) ? currentSpeed / movementSettings.RunSpeed : currentSpeed / movementSettings.WalkSpeed * .6f);
        animator.SetFloat("speedMove", animationSpeedPercent, movementSettings.SpeedSmoothTime, Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Rotate the body to aim in the forward direction
        if (isEquipped)
        {
            // Chest
            float xRot = chest.localEulerAngles.x + deltaX;
            float yRot = chest.localEulerAngles.y + deltaY;
            float zRot = chest.localEulerAngles.z;

            chest.transform.localEulerAngles = new Vector3(xRot, yRot, zRot);

            // UpperBody 0 - 55, 360 - 325
            xRot = Camera.main.transform.localEulerAngles.x;
            if (xRot > 300.0f)
            {
                xRot = Mathf.Clamp(xRot, 330.0f, 359.0f);
            }
            else
            {
                xRot = Mathf.Clamp(xRot, 0.0f, 50.0f);
            }

            Debug.Log("xRot: " + xRot);
            yRot = upperBody.localEulerAngles.x;
            zRot = upperBody.localEulerAngles.x;

            upperBody.localEulerAngles = new Vector3(xRot, yRot, zRot);
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero || isEquipped)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(movementSettings.TurnSmoothTime));

            // Trigger rotate animation
            if (isEquipped)
            {
                
            }
        }

        float targetSpeed = ((running) ? movementSettings.RunSpeed : movementSettings.WalkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(movementSettings.SpeedSmoothTime));

        velocityY += Time.deltaTime * movementSettings.Gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
            animator.SetBool("isJumping", false);
        }

    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * movementSettings.Gravity * movementSettings.JumpHeight * (currentSpeed / 20 + 5f));
            velocityY = jumpVelocity;
            animator.SetTrigger("jumpTrigger");
            animator.SetBool("isJumping", true);
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (movementSettings.AirControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / movementSettings.AirControlPercent;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
