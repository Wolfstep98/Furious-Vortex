using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Satellite : MonoBehaviour , IObject
{
    #region Fields & Properties
    [Header("Data")]
    [SerializeField]
    private float mass = 1.0f;
    public float Mass { get { return this.mass; } }

    [Header("References")]
    [SerializeField]
    new private Rigidbody2D rigidbody = null;
    public Rigidbody2D Rigidbody { get { return this.rigidbody; } }
	#endregion

	#region Methods
	#region Initializers
	private void Awake () 
	{
		
	}
	#endregion

	#endregion
}
