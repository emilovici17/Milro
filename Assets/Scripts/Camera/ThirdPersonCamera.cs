using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform normalTarget;
    public Transform aimTarget;

    [SerializeField]
    ThidPersonCamSettingsObject cameraSettings;

    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float yaw;
    private float pitch;
    private Transform target;

    void Start()
    {
        if (cameraSettings.LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        target = normalTarget;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * cameraSettings.MouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * cameraSettings.MouseSensitivity;
        pitch = Mathf.Clamp(pitch, cameraSettings.PitchMinMax.x, cameraSettings.PitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, cameraSettings.RotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * cameraSettings.DistancetFromTarget;
    }


    public void SwitchTarget()
    {
        if (target == normalTarget)
            target = aimTarget;
        else
            target = normalTarget;
    }
}