using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float deltaX = 0.0f;
    public float deltaY = 0.0f;

    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float pushForce = 3.0f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.2f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    Transform cameraT;
    CharacterController controller;
    [SerializeField]
    private GameObject chest;
    [SerializeField]
    private GameObject body;

    bool isEquipped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
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
            animator.SetInteger("weaponType", 2);
            Camera.main.GetComponent<ThirdPersonCamera>().SwitchTarget();


        }

        // animator
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .6f);
        animator.SetFloat("speedMove", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Rotate the body to aim in the forward direction
        if (isEquipped)
        {
            // Chest
            float xRot = chest.transform.localEulerAngles.x + deltaX;
            float yRot = chest.transform.localEulerAngles.y + deltaY;
            float zRot = chest.transform.localEulerAngles.z;           

            chest.transform.localEulerAngles = new Vector3(xRot, yRot, zRot);

            // Body 0 - 55, 360 - 325
            xRot = Camera.main.transform.localEulerAngles.x;
            if(xRot > 300.0f)
            {
                xRot = Mathf.Clamp(xRot, 330.0f, 359.0f);
            }
            else
            {
                xRot = Mathf.Clamp(xRot, 0.0f, 50.0f);
            }

            Debug.Log("xRot: " + xRot);
            yRot = body.transform.localEulerAngles.x;
            zRot = body.transform.localEulerAngles.x;

            body.transform.localEulerAngles = new Vector3(xRot, yRot, zRot);
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero || isEquipped)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));

            // Trigger rotate animation
            if (isEquipped)
            {
                
            }
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
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
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight * (currentSpeed / 20 + 5f));
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

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
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
