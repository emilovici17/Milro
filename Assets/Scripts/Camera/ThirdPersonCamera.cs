using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform normalTarget;
    public Transform aimTarget;

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = .12f;

    [SerializeField]
    ThidPersonCamSettingsObject cameraSettings;

    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float yaw;
    private float pitch;
    private Transform target;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        target = normalTarget;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }


    public void SwitchTarget()
    {
        if (target == normalTarget)
            target = aimTarget;
        else
            target = normalTarget;
    }
}