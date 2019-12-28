using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used for storing settings related to player/character/model moving
[CreateAssetMenu(fileName = "New MovementSettings", menuName = "Player/Settings/MovementSettings")]
[System.Serializable]
public class MovementSettingsObject : ScriptableObject
{
    #region PRIVATE

    [SerializeField]
    private float walkSpeed = 20.0f;
    
    [SerializeField]
    private float runSpeed = 50.0f;
    
    [SerializeField]
    private float gravity = -200.0f;
    
    [SerializeField]
    private float jumpHeight = 2.0f;
    
    [SerializeField]
    [Range(0, 1)]
    private float airControlPercent = 0.3f;

    [SerializeField]
    private float turnSmoothTime = 0.1f;

    [SerializeField]
    private float speedSmoothTime = 0.09f;

    #endregion

    #region PUBLIC

    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public float Gravity => gravity;
    public float JumpHeight => jumpHeight;
    public float AirControlPercent => airControlPercent;
    public float TurnSmoothTime => turnSmoothTime;
    public float SpeedSmoothTime => speedSmoothTime;

    #endregion
}
