using UnityEngine;

[CreateAssetMenu(fileName = "New ThirdPersonCameraSettings", menuName = "Camera/ThirdPersonCameraSettings")]
[System.Serializable]
public class ThidPersonCamSettingsObject : ScriptableObject
{
    #region PRIVATE

    [SerializeField]
    private Transform normalTarget;

    [SerializeField]
    private Transform aimTarget;

    [SerializeField]
    private bool lockCursor = false;

    [SerializeField]
    private float mouseSensitivity = 10;

    [SerializeField]
    private float distanceFromTarget = 25;

    [SerializeField]
    private Vector2 pitchMinMax = new Vector2(-40, 85);

    [SerializeField]
    private float rotationSmoothTime = .12f;

    #endregion

    #region PUBLIC

    public Transform NormalTarget => normalTarget;

    public Transform AimTarget => aimTarget;

    public bool LockCursor => lockCursor;

    public float MouseSensitivity => mouseSensitivity;

    public float DistancetFromTarget => distanceFromTarget;

    public Vector2 PitchMinMax => pitchMinMax;

    public float RotationSmoothTime =>rotationSmoothTime;

    #endregion
}
