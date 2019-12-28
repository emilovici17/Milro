using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public PlayerModelObject PlayerModel;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the player prefab as a playerController child
        Instantiate(PlayerModel.ModelPrefab, playerController.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
