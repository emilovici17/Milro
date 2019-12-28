using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItemObject : ItemObject
{
    #region PRIVATE

    /// <summary>
    /// The type of the gun
    /// </summary>
    [SerializeField]
    protected GunType gunType = GunType.Default;
    
    /// <summary>
    /// The damage done by a single shot
    /// </summary>
    [SerializeField]
    [Min(0)]
    private int damage = 0;
    
    /// <summary>
    /// The fire range of the gun
    /// </summary>
    [SerializeField]
    [Min(1)]
    private int fireRange = 1;

    /// <summary>
    /// Number of seconds passed between two consecutive shots
    /// </summary>
    [SerializeField]
    [Min(0.001f)]
    private float fireRate = 1.0f;

    #endregion

    #region PUBLIC

    #region INITIALIZATION

    public void Awake()
    {
        itemType = ItemType.Gun;
        isStackable = false;
    }

    #endregion

    public GunType GunType => gunType;

    public int Damage => damage;

    public int FireRange => fireRange;

    public float FireRate => fireRate;

    #endregion
}
