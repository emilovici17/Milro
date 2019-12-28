using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelBodyParts : MonoBehaviour
{
    [SerializeField]
    private Transform upperBody;
    [SerializeField]
    private Transform chest;

    public Transform UpperBody => upperBody;
    public Transform Chest => chest;
}
