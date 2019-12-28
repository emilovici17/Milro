using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

// Class used for storing player model
[CreateAssetMenu(fileName = "New PlayerModel", menuName = "Player/Models/Model")]
[System.Serializable]
public class PlayerModelObject : ScriptableObject
{
    public GameObject ModelPrefab;
}
