using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour , IObject
{
    #region Fields & Properties
    [Header("Data")]
    [SerializeField]
    private float mass = 10.0f;
    public float Mass { get { return this.mass; } }
    [SerializeField]
    private float gravity = 9.81f;
    public float Gravity { get { return this.gravity; } }
    #endregion

    #region Methods

	#endregion
}
