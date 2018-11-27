using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour 
{
    #region Fields & Properties
    [Header("Parameters")]
    [SerializeField]
    private bool isOrbiting = false;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private Vector3 direction = Vector3.zero;

    [Header("Orbit")]
    [SerializeField]
    private float radius = 0.0f;
    [SerializeField]
    private float periodeOrbitaire = 0.0f;

    [Header("References")]
    [SerializeField]
    new private Rigidbody2D rigidbody = null;
    public Rigidbody2D Rigidbody { get { return this.rigidbody; } }
	#endregion

	#region Methods
	#region Initializers
	private void Awake () 
	{
        this.Initialize();	
	}

    private void Initialize()
    {
#if UNITY_EDITOR

#endif
    }

    public void SetupBall(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
    }

    private void Start()
    {
        this.LaunchBall(Vector3.up);
    }

    public void LaunchBall(Vector3 direction)
    {
        this.direction = direction;
        this.rigidbody.velocity = direction * this.speed;
    }
	#endregion
	
	public void CustomUpdate () 
	{
		
	}

    public void CustomFixedUpdate()
    {
        if (this.isOrbiting)
        {
            this.rigidbody.velocity = this.direction * this.speed;
        }
        else
        {
            this.speed = (2 * Mathf.PI * this.radius) / this.periodeOrbitaire;
        }
    }
	#endregion
}
